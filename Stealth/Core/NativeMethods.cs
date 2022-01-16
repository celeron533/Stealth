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
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hwnd">A handle to the window. </param>
        /// <param name="lpdwProcessId">A pointer to a variable that receives the process identifier. 
        /// If this parameter is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; 
        /// otherwise, it does not. </param>
        /// <returns>The return value is the identifier of the thread that created the window. </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/ms633522%28v=vs.85%29.aspx</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability",
            "CA1401:PInvokesShouldNotBeVisible",
            Justification = "This method is needed for direct access.")]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>
        ///   <para>If the function succeeds, the return value is nonzero.</para>
        ///   <para>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
        /// </returns>
        /// <remarks>
        ///   <para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
        ///   <para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
        /// </remarks>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
    }
}
