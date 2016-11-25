using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Stealth.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

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
        private readonly IMainService _mainService;

        public ObservableCollection<WindowInfoItem> windowsInfoItemList { get; set; }

        #region Commands
        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(
                        () => _mainService.RefreshWindowData()
                        ));
            }
        }


        private RelayCommand<WindowInfoItem> _resetCommand;
        public RelayCommand<WindowInfoItem> ResetCommand
        {
            get
            {
                return _resetCommand
                    ?? (_resetCommand = new RelayCommand<WindowInfoItem>(
                        (item) => _mainService.ResetWindow(item)
                        ));
            }
        }

        private RelayCommand<TextBox> _titleFilterCommand;
        public RelayCommand<TextBox> TitleFilterCommand
        {
            get
            {
                return _titleFilterCommand
                    ?? (_titleFilterCommand = new RelayCommand<TextBox>(
                        (textbox) => _mainService.FilterByTitle(textbox.Text)
                        ));
            }
        }

        private RelayCommand<WindowInfoItem> _changeOpacityCommand;
        public RelayCommand<WindowInfoItem> ChangeOpacityCommand
        {
            get
            {
                return _changeOpacityCommand
                    ?? (_changeOpacityCommand = new RelayCommand<WindowInfoItem>(
                        (item) => _mainService.ChangeOpacity(item)
                        ));
            }
        }

        private RelayCommand<WindowInfoItem> _setTopMostCommand;
        public RelayCommand<WindowInfoItem> SetTopMostCommand
        {
            get
            {
                return _setTopMostCommand
                    ?? (_setTopMostCommand = new RelayCommand<WindowInfoItem>(
                        (item) => _mainService.SetTopMost(item)
                        ));
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
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