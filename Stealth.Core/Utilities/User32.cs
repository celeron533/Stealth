using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Stealth.Core.Utilities
{
    public partial class User32
    {
        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer. 
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpWindowText">The buffer that will receive the text.</param>
        /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character.</param>
        /// <returns>Text length</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);


        /// <summary>
        /// Enumerates all top-level windows associated with the specified desktop.
        /// </summary>
        /// <param name="hDesktop">[Optional] A handle to the desktop whose top-level windows are to be enumerated. </param>
        /// <param name="lpEnumCallbackFunction">A pointer (EnumDelegate) to an application-defined EnumWindowsProc callback function.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);


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
        [DllImport("user32.dll", EntryPoint = "GetLayeredWindowAttributes",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hWnd, out uint crKey, out byte bAlpha, out uint dwFlags);


        /// <summary>
        /// Changes an attribute of the specified window.
        /// The function also sets a value at the specified offset in the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified offset.
        /// If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern long SetWindowLongPtr(IntPtr hWnd, int nIndex, long dwNewLong);


        /// <summary>
        /// Get an attribute of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved. </param>
        /// <returns>the requested value</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll", EntryPoint ="SetWindowPos",
            ExactSpelling =false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);


        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>HWND</returns>
        [DllImport("user32.dll", EntryPoint = "GetActiveWindow",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern long GetActiveWindow();
    }
}
