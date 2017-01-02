﻿using GalaSoft.MvvmLight;
using Stealth.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.ViewModel
{
    public class WindowInfoItemModel : ViewModelBase
    {
        //// Base Properties

        private int _hWnd;
        public int hWnd
        {
            get { return _hWnd; }
            set { Set(ref _hWnd, value); }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private int _opacity;
        public int opacity
        {
            get { return _opacity; }
            set { Set(ref _opacity, value); }
        }

        private bool _isTopMost;
        public bool isTopMost
        {
            get { return _isTopMost; }
            set { Set(ref _isTopMost, value); }
        }


        //// Extended Properties ////

        private bool _isModified;
        public bool isModified
        {
            get { return _isModified; }
            set { Set(ref _isModified, value); }
        }

        private bool _isRemoved;
        public bool isRemoved
        {
            get { return _isRemoved; }
            set { Set(ref _isRemoved, value); }
        }

        // filters
        private bool _isTitleFilteredVisible;
        public bool isTitleFilteredVisible
        {
            get { return _isTitleFilteredVisible; }
            set
            {
                Set(ref _isTitleFilteredVisible, value);
                UpdateVisibility();
            }
        }

        private bool _isIncludeEmptyTitleVisible;
        public bool isIncludeEmptyTitleVisible
        {
            get { return _isIncludeEmptyTitleVisible; }
            set
            {
                Set(ref _isIncludeEmptyTitleVisible, value);
                UpdateVisibility();
            }
        }

        private bool _isIncludeRemovedVisible;
        public bool isIncludeRemovedVisible
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
        public bool isVisible
        {
            get { return _isVisible; }
            set { Set(ref _isVisible, value); }
        }

        /// <summary>
        /// Will be called when global setting changed. Such as 'hide all removed windows'
        /// </summary>
        public void UpdateVisibility()
        {
            isVisible = isTitleFilteredVisible &&
                        (isIncludeRemovedVisible || !isRemoved) &&
                        (isIncludeEmptyTitleVisible || !string.IsNullOrWhiteSpace(title));
        }

        /// <summary>
        /// Copy the base properties from native entity.
        /// </summary>
        /// <param name="nativeSource">Native entity</param>
        public void CopyFrom(WindowInstanceInfo nativeSource)
        {
            hWnd = nativeSource.hWnd.ToInt32();
            title = nativeSource.title;
            opacity = nativeSource.bAlpha;
            isTopMost = nativeSource.isTopMost;
        }


        public override string ToString()
        {
            return string.Format($"hWnd={_hWnd}, title={_title}, opacity={_opacity}");
        }
    }
}
