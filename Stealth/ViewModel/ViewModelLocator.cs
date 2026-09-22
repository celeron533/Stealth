using Stealth.Model;

namespace Stealth.ViewModel
{
    /// <summary>
    /// This class provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IMainService _mainService;
        private readonly IAboutService _aboutService;
        private readonly MainViewModel _mainViewModel;
        private readonly AboutViewModel _aboutViewModel;

        public ViewModelLocator()
        {
            var isDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

            if (isDesignMode)
            {
                _mainService = new Design.DesignMainService();
                _aboutService = new Design.DesignAboutService();
            }
            else
            {
                _mainService = new MainService();
                _aboutService = new AboutService();
            }

            _mainViewModel = new MainViewModel(_mainService);
            _aboutViewModel = new AboutViewModel(_aboutService);
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
                return _mainViewModel;
            }
        }

        public AboutViewModel About
        {
            get
            {
                return _aboutViewModel;
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