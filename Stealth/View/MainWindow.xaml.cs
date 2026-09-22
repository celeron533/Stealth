using System;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Stealth.ViewModel;

namespace Stealth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            WeakReferenceMessenger.Default.Register<ShowAboutViewMessage>(this, (recipient, msg) => ((MainWindow)recipient).NotificationMessageReceived(msg));
        }

        private void NotificationMessageReceived(ShowAboutViewMessage msg)
        {
            if (msg.Value == "ShowAboutView")
            {
                new AboutView().ShowDialog();
            }
        }
    }
}