using Stealth.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Stealth.Model
{
    public class OverlayService : IDisposable
    {
        private const double OverlayTopInset = 6d;
        private const double OverlayRightInset = 150d;
        private const double OverlayLeftPadding = 8d;

        private readonly DispatcherTimer _timer;
        private OverlayWindow _overlayWindow;
        private IntPtr _overlayHandle;
        private IntPtr _currentTargetHandle;
        private IntPtr _lastAnchoredTargetHandle;
        private bool _disposed;

        public OverlayService()
        {
            _timer = new DispatcherTimer(
                TimeSpan.FromMilliseconds(150),
                DispatcherPriority.Background,
                OnTimerTick,
                Application.Current.Dispatcher);
        }

        public void Start()
        {
            if (_overlayWindow != null)
            {
                return;
            }

            _overlayWindow = new OverlayWindow();
            _overlayWindow.SourceInitialized += OverlayWindow_SourceInitialized;
            _overlayWindow.Closed += OverlayWindow_Closed;
            _overlayWindow.OpacityChanged += OverlayWindow_OpacityChanged;
            _overlayWindow.Show();
            _overlayWindow.Hide();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();

            if (_overlayWindow != null)
            {
                _overlayWindow.OpacityChanged -= OverlayWindow_OpacityChanged;
                _overlayWindow.SourceInitialized -= OverlayWindow_SourceInitialized;
                _overlayWindow.Closed -= OverlayWindow_Closed;
                _overlayWindow.Close();
                _overlayWindow = null;
            }

            _overlayHandle = IntPtr.Zero;
            _currentTargetHandle = IntPtr.Zero;
        }

        private HWND ToHwnd(IntPtr handle)
        {
            unsafe
            {
                return new HWND((void*)handle);
            }
        }

        private bool TryGetWindowRect(IntPtr handle, out RECT rect)
        {
            return PInvoke.GetWindowRect(ToHwnd(handle), out rect);
        }

        private bool IsWindowUsable(IntPtr handle)
        {
            var hwnd = ToHwnd(handle);
            return PInvoke.IsWindow(hwnd) && !PInvoke.IsIconic(hwnd);
        }

        private IntPtr GetForegroundHandle()
        {
            return (IntPtr)PInvoke.GetForegroundWindow();
        }

        private bool IsOverlayHandle(IntPtr handle)
        {
            return handle != IntPtr.Zero && handle == _overlayHandle;
        }

        private bool IsWindowVisible(IntPtr handle)
        {
            return PInvoke.IsWindowVisible(ToHwnd(handle));
        }

        private bool IsValidTargetHandle(IntPtr handle)
        {
            return handle != IntPtr.Zero && !IsOwnWindow(handle) && IsWindowUsable(handle) && IsWindowVisible(handle);
        }

        private IntPtr GetPinnedTargetHandle()
        {
            return IsValidTargetHandle(_currentTargetHandle) ? _currentTargetHandle : IntPtr.Zero;
        }

        private bool CanKeepPinnedTarget()
        {
            return _overlayWindow != null && _overlayWindow.IsInteracting;
        }

        private void ClearCurrentTarget()
        {
            _currentTargetHandle = IntPtr.Zero;
        }

        private void OverlayWindow_SourceInitialized(object sender, EventArgs e)
        {
            _overlayHandle = new WindowInteropHelper(_overlayWindow).Handle;
        }

        private void OverlayWindow_Closed(object sender, EventArgs e)
        {
            _overlayHandle = IntPtr.Zero;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_overlayWindow == null)
            {
                return;
            }

            var targetHandle = ResolveTargetHandle();
            if (targetHandle == IntPtr.Zero || !TryGetWindowRect(targetHandle, out RECT rect))
            {
                HideOverlay();
                return;
            }

            using (var targetWindow = WindowInstanceInfo.CreateFromHandle(targetHandle, true, false))
            {
                if (targetWindow == null || !targetWindow.IsVisible)
                {
                    HideOverlay();
                    return;
                }

                if (targetHandle != _lastAnchoredTargetHandle)
                {
                    _overlayWindow.ResetManualOffset();
                    _lastAnchoredTargetHandle = targetHandle;
                }

                _currentTargetHandle = targetHandle;
                _overlayWindow.SetOpacityValue(targetWindow.BAlpha);
                UpdateOverlayPosition(rect);
                ShowOverlay();
            }
        }

        private IntPtr ResolveTargetHandle()
        {
            var foregroundHandle = GetForegroundHandle();
            if (foregroundHandle == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            if (IsOverlayHandle(foregroundHandle))
            {
                return GetPinnedTargetHandle();
            }

            if (IsOwnWindow(foregroundHandle))
            {
                return CanKeepPinnedTarget() ? GetPinnedTargetHandle() : IntPtr.Zero;
            }

            return IsValidTargetHandle(foregroundHandle) ? foregroundHandle : IntPtr.Zero;
        }

        private void OverlayWindow_OpacityChanged(int opacity)
        {
            if (_currentTargetHandle == IntPtr.Zero)
            {
                return;
            }

            using (var targetWindow = WindowInstanceInfo.CreateFromHandle(_currentTargetHandle, true, false))
            {
                if (targetWindow == null || !targetWindow.IsVisible)
                {
                    HideOverlay();
                    return;
                }

                targetWindow.IsLayered = true;
                targetWindow.BAlpha = (byte)Math.Max(byte.MinValue, Math.Min(byte.MaxValue, opacity));
                targetWindow.DwFlags = (uint)LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_ALPHA;
                targetWindow.CommitChanges();
            }
        }

        private bool IsOwnWindow(IntPtr handle)
        {
            return Application.Current.Windows
                .OfType<Window>()
                .Any(window => new WindowInteropHelper(window).Handle == handle);
        }

        private void UpdateOverlayPosition(RECT rect)
        {
            var transform = GetTransformFromDevice();
            var topLeft = transform.Transform(new Point(rect.left, rect.top));
            var topRight = transform.Transform(new Point(rect.right, rect.top));
            var anchorWidth = _overlayWindow.AnchorWidth;

            var left = Math.Max(topLeft.X + OverlayLeftPadding, topRight.X - anchorWidth - OverlayRightInset);
            var top = topLeft.Y + OverlayTopInset;

            _overlayWindow.SetAnchorPosition(left, top);
        }

        private Matrix GetTransformFromDevice()
        {
            var presentationSource = PresentationSource.FromVisual(_overlayWindow);
            return presentationSource?.CompositionTarget?.TransformFromDevice ?? Matrix.Identity;
        }

        private void ShowOverlay()
        {
            if (!_overlayWindow.IsVisible)
            {
                _overlayWindow.Show();
            }
        }

        private void HideOverlay()
        {
            ClearCurrentTarget();
            _lastAnchoredTargetHandle = IntPtr.Zero;
            if (_overlayWindow != null && _overlayWindow.IsVisible)
            {
                _overlayWindow.ResetManualOffset();
                _overlayWindow.Hide();
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Stop();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
