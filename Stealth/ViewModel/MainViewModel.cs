using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Stealth.Model;
using Stealth.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        public ObservableCollection<WindowInfoItemModel> windowsInfoItemList { get; }
        public ICollectionView WindowsView { get; }

        private string _titleFilterText;
        public string TitleFilterText
        {
            get { return _titleFilterText; }
            set
            {
                if (SetProperty(ref _titleFilterText, value))
                {
                    WindowsView.Refresh();
                }
            }
        }

        private bool _includeEmptyTitle;
        public bool includeEmptyTitle
        {
            get { return _includeEmptyTitle; }
            set
            {
                if (SetProperty(ref _includeEmptyTitle, value))
                {
                    WindowsView.Refresh();
                }
            }
        }

        private bool _includeRemoved;
        public bool includeRemoved
        {
            get { return _includeRemoved; }
            set
            {
                if (SetProperty(ref _includeRemoved, value))
                {
                    WindowsView.Refresh();
                }
            }
        }

        private bool _isOverlayEnabled;
        public bool IsOverlayEnabled
        {
            get { return _isOverlayEnabled; }
            set
            {
                if (SetProperty(ref _isOverlayEnabled, value))
                {
                    Settings.Default.OverlayEnabled = value;
                    Settings.Default.Save();
                    ((App)Application.Current)?.SetOverlayEnabled(value);
                }
            }
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
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMainService mainService)
        {
            _mainService = mainService;
            _isOverlayEnabled = Settings.Default.OverlayEnabled;
            windowsInfoItemList = _mainService.GetWindowListData();
            WindowsView = CollectionViewSource.GetDefaultView(windowsInfoItemList);
            WindowsView.Filter = IsWindowVisible;
        }

        public void UpdateOpacityFromOverlay(IntPtr handle, byte opacity)
        {
            _mainService.UpdateOpacityFromOverlay(handle, opacity);
        }

        private bool IsWindowVisible(object value)
        {
            var item = value as WindowInfoItemModel;
            return item != null
                && (includeRemoved || !item.IsRemoved)
                && (includeEmptyTitle || !string.IsNullOrWhiteSpace(item.Title))
                && (string.IsNullOrWhiteSpace(TitleFilterText)
                    || (!string.IsNullOrWhiteSpace(item.Title)
                        && item.Title.IndexOf(TitleFilterText, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        private void RefreshWindows()
        {
            _mainService.RefreshWindowData();
            WindowsView.Refresh();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}