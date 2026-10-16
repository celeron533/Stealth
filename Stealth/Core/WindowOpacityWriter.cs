using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Stealth.Core
{
    internal interface IWindowOpacityApi
    {
        int GetExtendedStyle(IntPtr handle);
        bool TryGetLayeredAttributes(IntPtr handle, out uint colorKey, out byte alpha, out uint flags, out int errorCode);
        bool TrySetExtendedStyle(IntPtr handle, int style, out int errorCode);
        bool TrySetLayeredAttributes(IntPtr handle, uint colorKey, byte alpha, uint flags, out int errorCode);
    }

    internal sealed class WindowOpacityWriter
    {
        private const uint LayeredAlpha = (uint)LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_ALPHA;
        private const uint LayeredColorKey = (uint)LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_COLORKEY;
        private const int LayeredStyle = (int)WINDOW_EX_STYLE.WS_EX_LAYERED;

        private readonly IWindowOpacityApi _api;

        internal WindowOpacityWriter(IWindowOpacityApi api)
        {
            _api = api;
        }

        internal bool TrySetOpacity(IntPtr handle, byte alpha, out string error)
        {
            var style = _api.GetExtendedStyle(handle);
            var isLayered = (style & LayeredStyle) != 0;
            uint colorKey = 0;
            byte currentAlpha = byte.MaxValue;
            uint existingFlags = 0;

            var attributesLoaded = _api.TryGetLayeredAttributes(handle, out colorKey, out currentAlpha, out existingFlags, out var readError);
            var attributesUnknown = isLayered && !attributesLoaded;

            if (!isLayered)
            {
                colorKey = 0;
                existingFlags = 0;
            }
            else if (attributesLoaded)
            {
                existingFlags &= LayeredColorKey;
            }
            else
            {
                colorKey = 0;
                existingFlags = 0;
            }

            var enabledLayeredStyle = false;
            if (!isLayered)
            {
                if (!_api.TrySetExtendedStyle(handle, style | LayeredStyle, out var styleError))
                {
                    error = $"Could not enable WS_EX_LAYERED (Win32 error {styleError}).";
                    return false;
                }

                enabledLayeredStyle = true;
            }

            var flags = LayeredAlpha | existingFlags;
            if (_api.TrySetLayeredAttributes(handle, colorKey, alpha, flags, out var alphaError))
            {
                error = attributesUnknown
                    ? $"Best-effort opacity update succeeded, but existing layered attributes could not be read (Win32 error {readError}). The target may use WPF per-pixel transparency, which can conflict with this update."
                    : null;
                return true;
            }

            var rollbackError = 0;
            var rollbackFailed = enabledLayeredStyle && !_api.TrySetExtendedStyle(handle, style, out rollbackError);
            error = $"SetLayeredWindowAttributes failed (Win32 error {alphaError}).";
            if (attributesUnknown)
            {
                error += $" Existing layered attributes could not be read (Win32 error {readError}); the target may use WPF per-pixel transparency.";
            }

            if (rollbackFailed)
            {
                error += $" Restoring the original window style also failed (Win32 error {rollbackError}).";
            }

            return false;
        }
    }

    internal sealed class NativeWindowOpacityApi : IWindowOpacityApi
    {
        public int GetExtendedStyle(IntPtr handle)
        {
            return PInvoke.GetWindowLong(ToHwnd(handle), WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        }

        public bool TryGetLayeredAttributes(IntPtr handle, out uint colorKey, out byte alpha, out uint flags, out int errorCode)
        {
            COLORREF nativeColorKey = default;
            LAYERED_WINDOW_ATTRIBUTES_FLAGS nativeFlags = 0;
            Span<byte> nativeAlpha = stackalloc byte[1];
            nativeAlpha[0] = byte.MaxValue;
            var succeeded = PInvoke.GetLayeredWindowAttributes(ToHwnd(handle), out nativeColorKey, nativeAlpha, out nativeFlags);
            colorKey = nativeColorKey;
            alpha = nativeAlpha[0];
            flags = (uint)nativeFlags;
            errorCode = succeeded ? 0 : Marshal.GetLastWin32Error();
            return succeeded;
        }

        public bool TrySetExtendedStyle(IntPtr handle, int style, out int errorCode)
        {
            PInvoke.SetWindowLong(ToHwnd(handle), WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, style);
            var currentStyle = GetExtendedStyle(handle);
            var succeeded = currentStyle == style;
            errorCode = succeeded ? 0 : Marshal.GetLastWin32Error();
            return succeeded;
        }

        public bool TrySetLayeredAttributes(IntPtr handle, uint colorKey, byte alpha, uint flags, out int errorCode)
        {
            var succeeded = PInvoke.SetLayeredWindowAttributes(
                ToHwnd(handle),
                (COLORREF)colorKey,
                alpha,
                (LAYERED_WINDOW_ATTRIBUTES_FLAGS)flags);
            errorCode = succeeded ? 0 : Marshal.GetLastWin32Error();
            return succeeded;
        }

        private static HWND ToHwnd(IntPtr handle)
        {
            unsafe
            {
                return new HWND((void*)handle);
            }
        }
    }
}