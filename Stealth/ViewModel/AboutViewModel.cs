using GalaSoft.MvvmLight;
using Stealth.Model;

namespace Stealth.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        private readonly IAboutService _aboutService;

        private string _version;
        public string version
        {
            get { return _version; }
            set { Set(ref _version, value); }
        }

        /// <summary>
        /// Initializes a new instance of the AboutViewModel class.
        /// </summary>
        public AboutViewModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
            version = aboutService.GetVersionString();
        }
    }
}