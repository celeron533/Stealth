using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Core
{
    /// <summary>
    /// This class implements some endpoints which are missing in PInvoke.User32
    /// </summary>
    public static class NativeMethods
    {
        #region LayeredWindowAttributes
        /// <summary>
        /// Sets the opacity and transparency color key of a layered window
        /// </summary>
        /// <param name="hWnd">A handle to the layered window</param>
        /// <param name="crKey">RGB: such as 0xFFAA00</param>
        /// <param name="bAlpha">When bAlpha is 0, the window is completely transparent.
        /// When bAlpha is 255, the window is opaque.</param>
        /// <param name="dwFlags">An action to be taken. This parameter can be one or more of the following values.
        /// LWA_ALPHA: 0x2
        /// LWA_COLORKEY: 0x1</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability",
            "CA1401:PInvokesShouldNotBeVisible",
            Justification = "This method is needed for direct access.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability",
            "CA1901:PInvokeDeclarationsShouldBePortable",
            Justification = "bAlpha range: 0 to 255.")]
        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Gets the opacity and transparency color key of a layered window
        /// </summary>
        /// <param name="hWnd">A handle to the layered window</param>
        /// <param name="crKey">RGB: such as 0xFFAA00</param>
        /// <param name="bAlpha">When bAlpha is 0, the window is completely transparent.
        /// When bAlpha is 255, the window is opaque.</param>
        /// <param name="dwFlags">An action to be taken. This parameter can be one or more of the following values.
        /// LWA_ALPHA: 0x2
        /// LWA_COLORKEY: 0x1</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability",
            "CA1401:PInvokesShouldNotBeVisible",
            Justification = "This method is needed for direct access.")]
        [DllImport("user32.dll", EntryPoint = "GetLayeredWindowAttributes",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hWnd, out uint crKey, out byte bAlpha, out uint dwFlags);
        #endregion

        [Flags]
        public enum LWA : uint
        {
            /// <summary>
            /// Default value.
            /// </summary>
            LWA_UNDEFINED = 0x0,
            /// <summary>
            /// Use crKey as the transparency color.
            /// </summary>
            LWA_COLOYKEY = 0x1,
            /// <summary>
            /// Use bAlpha to determine the opacity of the layered window.
            /// </summary>
            LWA_ALPHA = 0x2
        }

        /// <summary>
        /// Special window handles
        /// </summary>
        public enum SpecialWindowHandles : int
        {
            /// <summary>
            /// Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,

            /// <summary>
            /// Places the window at the bottom of the Z order.
            /// If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,

            /// <summary>
            /// Places the window above all non-topmost windows.
            /// The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,

            /// <summary>
            /// Places the window above all non-topmost windows (that is, behind all topmost windows).
            /// This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>
            /// If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window.
            /// This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            /// Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            /// Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            /// Applies new frame styles set using the SetWindowLong function.
            /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
            /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            /// Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            /// Does not activate the window.
            /// If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            /// Discards the entire contents of the client area.
            /// If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            /// Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            /// Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            /// Does not redraw changes.
            /// If this flag is set, no repainting of any kind occurs.
            /// This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved.
            /// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            /// Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            /// Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            /// Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            /// Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,
        }
    }
}
