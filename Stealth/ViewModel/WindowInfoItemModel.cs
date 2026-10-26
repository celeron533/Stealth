using CommunityToolkit.Mvvm.ComponentModel;
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
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;

namespace Stealth.ViewModel
{
    public class WindowInfoItemModel : ObservableObject
    {
        //// Base Properties

        private IntPtr _hWnd;
        public IntPtr HWnd
        {
            get { return _hWnd; }
            set { SetProperty(ref _hWnd, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _opacity;
        public int Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }

        private string _opacityError;
        public string OpacityError
        {
            get { return _opacityError; }
            set { SetProperty(ref _opacityError, value); }
        }

        private bool _isTopMost;
        public bool IsTopMost
        {
            get { return _isTopMost; }
            set { SetProperty(ref _isTopMost, value); }
        }

        private ImageSource _procIcon;
        public ImageSource ProcIcon
        {
            get { return _procIcon; }
            set { SetProperty(ref _procIcon, value); }
        }


        //// Extended Properties ////

        private bool _isModified;
        public bool IsModified
        {
            get { return _isModified; }
            set { SetProperty(ref _isModified, value); }
        }

        private bool _isRemoved;
        public bool IsRemoved
        {
            get { return _isRemoved; }
            set { SetProperty(ref _isRemoved, value); }
        }

        private Process _process;

        internal bool IsUpdatingFromNative { get; private set; }

        internal void UpdateOpacityFromNative(byte opacity)
        {
            IsUpdatingFromNative = true;
            try
            {
                Opacity = opacity;
                OpacityError = null;
            }
            finally
            {
                IsUpdatingFromNative = false;
            }
        }

        /// <summary>
        /// Copy the base properties from native entity.
        /// </summary>
        /// <param name="nativeSource">Native entity</param>
        public void CopyFrom(WindowInstanceInfo nativeSource)
        {
            IsUpdatingFromNative = true;
            try
            {
                HWnd = (IntPtr)nativeSource.HWnd;
                Title = nativeSource.Title;
                Opacity = nativeSource.BAlpha;
                IsTopMost = nativeSource.IsTopMost;
                _process = nativeSource.process;
                ProcIcon = LoadBitmap(nativeSource.iconBitmap);
            }
            finally
            {
                IsUpdatingFromNative = false;
            }
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
                PInvoke.DeleteObject((HGDIOBJ)ip);
            }

            return bitmapSource;
        }

        public override string ToString()
        {
            return $"hWnd=0x{_hWnd.ToInt64():X}, title={_title}, opacity={_opacity}";
        }
    }
}
