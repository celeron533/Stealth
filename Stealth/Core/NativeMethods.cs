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

    }
}
