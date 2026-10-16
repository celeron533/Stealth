using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Stealth
{
    public partial class OverlayWindow : Window
    {
        private static readonly Duration ExpandAnimationDuration = new Duration(TimeSpan.FromMilliseconds(160));
        private static readonly Duration FadeAnimationDuration = new Duration(TimeSpan.FromMilliseconds(120));
        private static readonly TimeSpan HoverExpandDelay = TimeSpan.FromSeconds(1);
        private const double CollapsedSize = 32d;
        private const double ExpandedWidth = 208d;
        private const double ExpandedHeight = 32d;
        private const double CollapsedRootOpacity = 0.92d;
        private const double ExpandedRootOpacity = 1d;

        private readonly DispatcherTimer _hoverExpandTimer;
        private bool _isUpdating;
        private bool _isExpanded;
        private bool _isDragging;
        private Point _dragStartScreen;
        private Vector _dragStartOffset;
        private double _anchorLeft;
        private double _anchorTop;
        private Vector _manualOffset;

        public event Action<int> OpacityChanged;

        public OverlayWindow()
        {
            InitializeComponent();
            _hoverExpandTimer = new DispatcherTimer { Interval = HoverExpandDelay };
            _hoverExpandTimer.Tick += HoverExpandTimer_Tick;
            RootBorder.Opacity = CollapsedRootOpacity;
            Collapse();
        }

        public bool IsBeingDragged => _isDragging;

        public bool IsInteracting => IsMouseOver || _isDragging;

        public double AnchorWidth => CollapsedSize;

        public void SetOpacityValue(byte opacity)
        {
            _isUpdating = true;
            try
            {
                OpacitySlider.Value = opacity;
                OpacityValueText.Text = opacity.ToString();
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public void SetOpacityError(string error)
        {
            OpacitySlider.ToolTip = error;
        }

        public void SetAnchorPosition(double left, double top)
        {
            _anchorLeft = left;
            _anchorTop = top;

            if (!_isDragging)
            {
                ApplyPosition();
            }
        }

        public void ResetManualOffset()
        {
            _manualOffset = new Vector();

            if (!_isDragging)
            {
                ApplyPosition();
            }
        }

        private void Expand()
        {
            StopHoverExpandTimer();

            if (_isExpanded)
            {
                return;
            }

            _isExpanded = true;
            Height = ExpandedHeight;
            ExpandedPanel.Visibility = Visibility.Visible;
            ExpandedPanel.Opacity = 0d;
            RootBorder.Opacity = CollapsedRootOpacity;
            ApplyPosition();
            AnimateWidth(ExpandedWidth);
            AnimateRootOpacity(ExpandedRootOpacity);
            AnimatePanelOpacity(1d, null);
        }

        private void Collapse()
        {
            StopHoverExpandTimer();

            if (!_isExpanded)
            {
                BeginAnimation(WidthProperty, null);
                Width = CollapsedSize;
                Height = CollapsedSize;
                ExpandedPanel.Visibility = Visibility.Collapsed;
                ExpandedPanel.Opacity = 0d;
                RootBorder.Opacity = CollapsedRootOpacity;
                ApplyPosition();
                return;
            }

            _isExpanded = false;
            AnimateWidth(CollapsedSize);
            AnimateRootOpacity(CollapsedRootOpacity);
            AnimatePanelOpacity(0d, () =>
            {
                ExpandedPanel.Visibility = Visibility.Collapsed;
                Height = CollapsedSize;
                ApplyPosition();
            });
        }

        private void ApplyPosition()
        {
            Left = _anchorLeft + _manualOffset.X;
            Top = _anchorTop + _manualOffset.Y;
        }

        private void AnimateWidth(double targetWidth)
        {
            BeginAnimation(WidthProperty, null);
            var animation = new DoubleAnimation
            {
                From = Width,
                To = targetWidth,
                Duration = ExpandAnimationDuration,
                FillBehavior = FillBehavior.Stop
            };
            animation.Completed += (sender, e) => Width = targetWidth;
            BeginAnimation(WidthProperty, animation);
        }

        private void AnimateRootOpacity(double targetOpacity)
        {
            RootBorder.BeginAnimation(OpacityProperty, null);
            var animation = new DoubleAnimation
            {
                From = RootBorder.Opacity,
                To = targetOpacity,
                Duration = FadeAnimationDuration,
                FillBehavior = FillBehavior.Stop
            };
            animation.Completed += (sender, e) => RootBorder.Opacity = targetOpacity;
            RootBorder.BeginAnimation(OpacityProperty, animation);
        }

        private void AnimatePanelOpacity(double targetOpacity, Action completedAction)
        {
            ExpandedPanel.BeginAnimation(OpacityProperty, null);
            var animation = new DoubleAnimation
            {
                From = ExpandedPanel.Opacity,
                To = targetOpacity,
                Duration = FadeAnimationDuration,
                BeginTime = targetOpacity > 0d ? TimeSpan.FromMilliseconds(30) : TimeSpan.Zero,
                FillBehavior = FillBehavior.Stop
            };
            animation.Completed += (sender, e) =>
            {
                ExpandedPanel.Opacity = targetOpacity;
                completedAction?.Invoke();
            };
            ExpandedPanel.BeginAnimation(OpacityProperty, animation);
        }

        private void BadgeHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StopHoverExpandTimer();
            Expand();
            _isDragging = true;
            _dragStartScreen = PointToScreen(e.GetPosition(this));
            _dragStartOffset = _manualOffset;
            BadgeHost.CaptureMouse();
            e.Handled = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
            {
                return;
            }

            var currentScreen = PointToScreen(e.GetPosition(this));
            _manualOffset = _dragStartOffset + (currentScreen - _dragStartScreen);
            ApplyPosition();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDragging)
            {
                return;
            }

            _isDragging = false;
            BadgeHost.ReleaseMouseCapture();

            if (!IsMouseOver)
            {
                Collapse();
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_isDragging || _isExpanded)
            {
                return;
            }

            RestartHoverExpandTimer();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            StopHoverExpandTimer();

            if (_isDragging)
            {
                return;
            }

            Collapse();
        }

        private void HoverExpandTimer_Tick(object sender, EventArgs e)
        {
            StopHoverExpandTimer();

            if (IsMouseOver && !_isDragging)
            {
                Expand();
            }
        }

        private void RestartHoverExpandTimer()
        {
            _hoverExpandTimer.Stop();
            _hoverExpandTimer.Start();
        }

        private void StopHoverExpandTimer()
        {
            _hoverExpandTimer.Stop();
        }

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var opacity = (int)Math.Round(e.NewValue);
            OpacityValueText?.Text = opacity.ToString();

            if (_isUpdating)
            {
                return;
            }

            OpacityChanged?.Invoke(opacity);
        }
    }
}
