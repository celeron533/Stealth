using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth
{
    /// <summary>
    /// Define useful enum Flags of User32.dll
    /// </summary>
    public partial class User32
    {
        [Flags]
        public enum LWA
        {
            LWA_ALPHA = 0x2,  //Use bAlpha to determine the opacity of the layered window.
            LWA_COLOYKEY = 0x1  //Use crKey as the transparency color.
        }


        [Flags]
        public enum GWL
        {
            GWL_EXSTYLE = -20, //Sets a new extended window style.
            GWL_HINSTANCE = -6, //Sets a new application instance handle.
            GWL_ID = -12, //Sets a new identifier of the child window. The window cannot be a top-level window.
            GWL_STYLE = -16, //Sets a new window style.
            GWL_USERDATA = -21, //Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
            GWL_WNDPROC = -4 //Sets a new address for the window procedure. You cannot change this attribute if the window does not belong to the same process as the calling thread.
        }


        [Flags]
        public enum WS_EX
        {
            WS_EX_LAYERED = 0x80000 //The window is a layered window.
        }

    }
}
