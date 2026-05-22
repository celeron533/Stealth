/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Stealth.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using CommonServiceLocator;
using Stealth.Model;

namespace Stealth.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        private static readonly IMainService MainServiceInstance;
        private static readonly IAboutService AboutServiceInstance;
        private static readonly MainViewModel MainViewModelInstance;
        private static readonly AboutViewModel AboutViewModelInstance;

        static ViewModelLocator()
        {
            var isDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

            if (isDesignMode)
            {
                MainServiceInstance = new Design.DesignMainService();
                AboutServiceInstance = new Design.DesignAboutService();
            }
            else
            {
                MainServiceInstance = new MainService();
                AboutServiceInstance = new AboutService();
            }

            MainViewModelInstance = new MainViewModel(MainServiceInstance);
            AboutViewModelInstance = new AboutViewModel(AboutServiceInstance);
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainViewModelInstance;
            }
        }

        public AboutViewModel About
        {
            get
            {
                return AboutViewModelInstance;
            }
        }


        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}