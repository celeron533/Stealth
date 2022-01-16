using GalaSoft.MvvmLight;
using Stealth.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Stealth.ViewModel
{
    public class WindowInfoItemModel : ViewModelBase
    {
        //// Base Properties

        private int _hWnd;
        public int HWnd
        {
            get { return _hWnd; }
            set { Set(ref _hWnd, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private int _opacity;
        public int Opacity
        {
            get { return _opacity; }
            set { Set(ref _opacity, value); }
        }

        private bool _isTopMost;
        public bool IsTopMost
        {
            get { return _isTopMost; }
            set { Set(ref _isTopMost, value); }
        }

        private ImageSource _procIcon;
        public ImageSource ProcIcon
        {
            get { return _procIcon; }
            set { Set(ref _procIcon, value); }
        }


        //// Extended Properties ////

        private bool _isModified;
        public bool IsModified
        {
            get { return _isModified; }
            set { Set(ref _isModified, value); }
        }

        private bool _isRemoved;
        public bool IsRemoved
        {
            get { return _isRemoved; }
            set { Set(ref _isRemoved, value); }
        }

        private Process _process;

        // filters
        private bool _isTitleFilteredVisible;
        public bool IsTitleFilteredVisible
        {
            get { return _isTitleFilteredVisible; }
            set
            {
                Set(ref _isTitleFilteredVisible, value);
                UpdateVisibility();
            }
        }

        private bool _isIncludeEmptyTitleVisible;
        public bool IsIncludeEmptyTitleVisible
        {
            get { return _isIncludeEmptyTitleVisible; }
            set
            {
                Set(ref _isIncludeEmptyTitleVisible, value);
                UpdateVisibility();
            }
        }

        private bool _isIncludeRemovedVisible;
        public bool IsIncludeRemovedVisible
        {
            get { return _isIncludeRemovedVisible; }
            set
            {
                Set(ref _isIncludeRemovedVisible, value);
                UpdateVisibility();
            }
        }

        // TODO: Computed from isRemoved, isTitleFilteredVisible or other attributes based on settings.
        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { Set(ref _isVisible, value); }
        }

        /// <summary>
        /// Will be called when global setting changed. Such as 'hide all removed windows'
        /// </summary>
        public void UpdateVisibility()
        {
            IsVisible = IsTitleFilteredVisible &&
                        (IsIncludeRemovedVisible || !IsRemoved) &&
                        (IsIncludeEmptyTitleVisible || !string.IsNullOrWhiteSpace(Title));
        }

        /// <summary>
        /// Copy the base properties from native entity.
        /// </summary>
        /// <param name="nativeSource">Native entity</param>
        public void CopyFrom(WindowInstanceInfo nativeSource)
        {
            HWnd = nativeSource.HWnd.ToInt32();
            Title = nativeSource.Title;
            Opacity = nativeSource.BAlpha;
            IsTopMost = nativeSource.IsTopMost;
            _process = nativeSource.process;
            ProcIcon = LoadBitmap(nativeSource.iconBitmap);
        }


        /// <summary>
        /// Convert the Bitmap to BitmapSource
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BitmapSource LoadBitmap(Bitmap source)
        {
            if (source == null)
                return null;

            IntPtr ip = source.GetHbitmap();
            BitmapSource bitmapSource = null;
            try
            {
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                // prevent memory leak
                NativeMethods.DeleteObject(ip);
            }

            return bitmapSource;
        }

        public override string ToString()
        {
            return $"hWnd={_hWnd}, title={_title}, opacity={_opacity}";
        }
    }
}
