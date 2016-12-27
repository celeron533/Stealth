using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
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

        public ObservableCollection<WindowInfoItemModel> windowsInfoItemList { get; set; }

        private bool _includeEmptyTitle;
        public bool includeEmptyTitle
        {
            get { return _includeEmptyTitle; }
            set { Set(ref _includeEmptyTitle, value); }
        }

        private bool _includeRemoved;
        public bool includeRemoved
        {
            get { return _includeRemoved; }
            set { Set(ref _includeRemoved, value); }
        }

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

        private RelayCommand _aboutCommand;
        public RelayCommand AboutCommand
        {
            get
            {
                return _aboutCommand
                  ?? (_aboutCommand = new RelayCommand(
                      () => Messenger.Default.Send(new NotificationMessage("ShowAboutView"))
                      ));
            }
        }

        private RelayCommand _checkUpdateCommand;
        public RelayCommand CheckUpdateCommand
        {
            get
            {
                return _checkUpdateCommand
                    ?? (_checkUpdateCommand = new RelayCommand(
                        () => Messenger.Default.Send(new NotificationMessage("ShowUpdateView"))
                    ));
            }
        }

        //items
        private RelayCommand<WindowInfoItemModel> _detailCommand;
        public RelayCommand<WindowInfoItemModel> DetailCommand
        {
            get
            {
                return _detailCommand
                    ?? (_detailCommand = new RelayCommand<WindowInfoItemModel>(
                        (item) => _mainService.Detail(item)
                        ));
            }
        }


        private RelayCommand<WindowInfoItemModel> _changeOpacityCommand;
        public RelayCommand<WindowInfoItemModel> ChangeOpacityCommand
        {
            get
            {
                return _changeOpacityCommand
                    ?? (_changeOpacityCommand = new RelayCommand<WindowInfoItemModel>(
                        (item) => _mainService.ChangeOpacity(item)
                        ));
            }
        }

        private RelayCommand<WindowInfoItemModel> _setTopMostCommand;
        public RelayCommand<WindowInfoItemModel> SetTopMostCommand
        {
            get
            {
                return _setTopMostCommand
                    ?? (_setTopMostCommand = new RelayCommand<WindowInfoItemModel>(
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
            windowsInfoItemList = _mainService.GetWindowListData();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}