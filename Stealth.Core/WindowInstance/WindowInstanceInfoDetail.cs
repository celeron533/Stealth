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
            //todo: refresh other members
            transparencyProperty = new TransparencyProperty(hWnd);
        }
        public TransparencyProperty transparencyProperty;

        //local
        public bool isAlive { set; get; }  //if the windows is not destoried
        public bool isModified { set; get; }

        public bool isOnTop { set; get; }



        public override string ToString()
        {
            return hWnd + ", " + windowTitle + ", " + isWindowVisible;
        }



        public class TransparencyProperty
        {
            public TransparencyProperty(IntPtr hWnd)
            {
                _hWnd = hWnd;
                refreshAll();
            }
            private IntPtr _hWnd;

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

            public void refreshAll()
            {
                User32.GetLayeredWindowAttributes(_hWnd, out _crKey, out _bAlpha, out _dwFlags);
            }

            public void applyChanges()
            {
                User32.SetLayeredWindowAttributes(_hWnd, _crKey, _bAlpha, _dwFlags);
            }
        }


    }
}
