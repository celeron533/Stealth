using Stealth.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Core.WindowInstance
{
    public class WindowInstanceInfoDetail : WindowInstanceInfoBase
    {
        public WindowInstanceInfoDetail(IntPtr hWnd) : base(hWnd)
        {
            this.refreshAll();
        }

        public new void refreshAll()
        {
            base.refreshAll();
            transparencyProperty = new TransparencyProperty(hWnd);
            _extendedStyle = User32.GetWindowLongPtr(hWnd, (int)User32.GWL.GWL_EXSTYLE);
            //todo: refresh other members
        }

        public TransparencyProperty transparencyProperty;

        public bool isLayered
        {
            set
            {
                if (value)
                { extendedStyle |= (long)User32.WS_EX.WS_EX_LAYERED; }
                else
                { extendedStyle &= ~(long)User32.WS_EX.WS_EX_LAYERED; }
            }
            get { return (extendedStyle & (long)User32.WS_EX.WS_EX_LAYERED) != 0; }
        }

        private long _extendedStyle;
        public long extendedStyle
        {
            set
            {
                _extendedStyle = value;
                User32.SetWindowLongPtr(hWnd, (int)User32.GWL.GWL_EXSTYLE, _extendedStyle);
            }
            get { return _extendedStyle; }
        }

        //local
        public bool isAlive { set; get; }  //if the windows is not destoried
        public bool isModified { set; get; }
        public bool isOnTop { set; get; }


        public override string ToString()
        {
            return base.ToString() + "\n  " + transparencyProperty.ToString() + ", isLayered: " +isLayered;
        }



        public class TransparencyProperty
        {
            public TransparencyProperty(IntPtr hWnd)
            {
                _hWnd = hWnd;
                refreshAll();
            }
            private IntPtr _hWnd;

            #region members
            private uint _crKey;
            public uint crKey
            {
                set
                {
                    _crKey = value;
                    applyChanges();
                }
                get { return _crKey; }
            }

            private byte _bAlpha;
            public byte bAlpha
            {
                set
                {
                    _bAlpha = value;
                    applyChanges();
                }
                get { return _bAlpha; }
            }

            private uint _dwFlags;
            public uint dwFlags
            {
                set
                {
                    _dwFlags = value;
                    applyChanges();
                }
                get { return _dwFlags; }
            }


            #endregion

            public void refreshAll()
            {
                User32.GetLayeredWindowAttributes(_hWnd, out _crKey, out _bAlpha, out _dwFlags);
            }

            public void applyChanges()
            {
                User32.SetLayeredWindowAttributes(_hWnd, _crKey, _bAlpha, _dwFlags);
            }

            public override string ToString()
            {
                return string.Format("hWnd={0}, crKey={1}, bAlpha={2}, dwFlags={3}",
                    _hWnd, crKey, bAlpha, dwFlags);
            }
        }


    }
}
