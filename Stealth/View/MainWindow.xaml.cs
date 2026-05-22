using System.Windows;
using Stealth.ViewModel;
using CommunityToolkit.Mvvm.Messaging;
using System;

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

        private MainViewModel ViewModel => DataContext as MainViewModel;

        private void TextFilterInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ViewModel?.TitleFilterCommand.Execute((System.Windows.Controls.TextBox)sender);
        }

        private void IncludeRemovedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.IncludeRemovedCommand.Execute(((System.Windows.Controls.CheckBox)sender));
        }

        private void IncludeEmptyTitleCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.IncludeEmptyTitleCommand.Execute(((System.Windows.Controls.CheckBox)sender));
        }

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as System.Windows.Controls.Slider;
            var item = slider?.DataContext as WindowInfoItemModel;
            if (item != null)
            {
                ViewModel?.ChangeOpacityCommand.Execute(item);
            }
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