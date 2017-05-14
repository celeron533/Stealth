using System.Diagnostics;
using System.Windows;

namespace Stealth
{
    /// <summary>
    /// Description for AboutView.
    /// </summary>
    public partial class AboutView : Window
    {
        /// <summary>
        /// Initializes a new instance of the AboutView class.
        /// </summary>
        public AboutView()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}