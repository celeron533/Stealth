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
    public class UpdateViewModel : ViewModelBase
    {
        private readonly IUpdateService _updateService;
        /// <summary>
        /// Initializes a new instance of the UpdateViewModel class.
        /// </summary>
        public UpdateViewModel(IUpdateService updateService)
        {
            _updateService = updateService;
        }
    }
}