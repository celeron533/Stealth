using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Stealth.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Stealth.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //private readonly IDataService _dataService;
        private readonly IMainService _mainService;

        ///// <summary>
        ///// The <see cref="WelcomeTitle" /> property's name.
        ///// </summary>
        //public const string WelcomeTitlePropertyName = "WelcomeTitle";

        //private string _welcomeTitle = string.Empty;

        ///// <summary>
        ///// Gets the WelcomeTitle property.
        ///// Changes to that property's value raise the PropertyChanged event. 
        ///// </summary>
        //public string WelcomeTitle
        //{
        //    get
        //    {
        //        return _welcomeTitle;
        //    }
        //    set
        //    {
        //        Set(ref _welcomeTitle, value);
        //    }
        //}

        public ObservableCollection<WindowInfoItem> windowsInfoItemList { get; set; }


        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                    _refreshCommand = new RelayCommand(
                        () =>
                        {
                            _mainService.RefreshWindowData();
                        }
                        );
                return _refreshCommand;
            }
        }


        private RelayCommand _resetCommand;
        public RelayCommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(
                        () =>
                        {
                            _mainService.ResetWindow();
                        }
                        );
                return _resetCommand;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        //public MainViewModel(IDataService dataService)
        //{
        //    _dataService = dataService;
        //    _dataService.GetData(
        //        (item, error) =>
        //        {
        //            if (error != null)
        //            {
        //                // Report error here
        //                Debug.WriteLine(error.ToString());
        //                return;
        //            }

        //            WelcomeTitle = item.Title;
        //        });
        //}

        public MainViewModel(IMainService mainService)
        {
            _mainService = mainService;
            windowsInfoItemList = _mainService.GetWindowData();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}