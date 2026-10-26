using Stealth.Model;
using Stealth.Properties;
using Stealth.ViewModel;
using System;
using System.Windows;

namespace Stealth
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private OverlayService _overlayService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnMainWindowClose;

            var mainWindow = new MainWindow();
            MainWindow = mainWindow;
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.Show();

            SetOverlayEnabled(Settings.Default.OverlayEnabled);
        }

        public void SetOverlayEnabled(bool isEnabled)
        {
            if (isEnabled)
            {
                if (_overlayService == null)
                {
                    _overlayService = new OverlayService();
                    _overlayService.OpacityApplied += OverlayService_OpacityApplied;
                    _overlayService.Start();
                }

                return;
            }

            DisposeOverlayService();
        }

        private void OverlayService_OpacityApplied(IntPtr handle, byte opacity)
        {
            ((ViewModelLocator)Resources["Locator"]).Main.UpdateOpacityFromOverlay(handle, opacity);
        }

        private void DisposeOverlayService()
        {
            if (_overlayService == null)
            {
                return;
            }

            _overlayService.OpacityApplied -= OverlayService_OpacityApplied;
            _overlayService.Dispose();
            _overlayService = null;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            DisposeOverlayService();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DisposeOverlayService();
            base.OnExit(e);
        }
    }
}
