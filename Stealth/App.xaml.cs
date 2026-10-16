using Stealth.Model;
using Stealth.Properties;
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
                    _overlayService.Start();
                }

                return;
            }

            _overlayService?.Dispose();
            _overlayService = null;
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            _overlayService?.Dispose();
            _overlayService = null;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _overlayService?.Dispose();
            _overlayService = null;
            base.OnExit(e);
        }
    }
}
