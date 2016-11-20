using Stealth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public class WindowInfoItem : GalaSoft.MvvmLight.ViewModelBase
    {
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


        public void CopyFrom(WindowInstanceInfo nativeSource)
        {
            hWnd = nativeSource.hWnd.ToInt32();
            title = nativeSource.title;
            opacity = nativeSource.bAlpha;
        }


        public override string ToString()
        {
            return string.Format($"hWnd={_hWnd}, title={_title}, opacity={_opacity}");
        }
    }
}
