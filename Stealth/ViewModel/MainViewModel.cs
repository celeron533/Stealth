using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    public class MainViewModel : ObservableObject
    {
        private readonly IMainService _mainService;
        private ObservableCollection<WindowInfoItemModel> _windowsInfoItemList;
        public ObservableCollection<WindowInfoItemModel> windowsInfoItemList
        {
            get { return _windowsInfoItemList; }
            set { SetProperty(ref _windowsInfoItemList, value); }
        }

        private bool _includeEmptyTitle;
        public bool includeEmptyTitle
        {
            get { return _includeEmptyTitle; }
            set { SetProperty(ref _includeEmptyTitle, value); }
        }

        private bool _includeRemoved;
        public bool includeRemoved
        {
            get { return _includeRemoved; }
            set { SetProperty(ref _includeRemoved, value); }
        }

        #region Commands
        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(
                        () => RefreshWindows()
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
                        textbox => _mainService.FilterByTitle(textbox.Text)
                        ));
            }
        }

        private RelayCommand<CheckBox> _includeEmptyTitleCommand;
        public RelayCommand<CheckBox> IncludeEmptyTitleCommand
        {
            get
            {
                return _includeEmptyTitleCommand
                    ?? (_includeEmptyTitleCommand = new RelayCommand<CheckBox>(
                        checkbox => _mainService.FilterByIncludeEmptyTitle(checkbox.IsChecked)
                        ));
            }
        }

        private RelayCommand<CheckBox> _includeRemovedCommand;
        public RelayCommand<CheckBox> IncludeRemovedCommand
        {
            get
            {
                return _includeRemovedCommand
                    ?? (_includeRemovedCommand = new RelayCommand<CheckBox>(
                        checkbox => _mainService.FilterByIncludeRemoved(checkbox.IsChecked)
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
                      () => WeakReferenceMessenger.Default.Send(new ShowAboutViewMessage())
                      ));
            }
        }

        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand
                  ?? (_exitCommand = new RelayCommand(
                      () => System.Windows.Application.Current.Shutdown()
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
                        item => _mainService.Detail(item)
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
                        item => _mainService.ChangeOpacity(item)
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
                        item => _mainService.SetTopMost(item)
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

        private void RefreshWindows()
        {
            _mainService.RefreshWindowData();
            windowsInfoItemList = _mainService.GetWindowListData();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}