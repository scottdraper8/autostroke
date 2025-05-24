using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoStroke
{
    /// <summary>
    /// Provides functionality to set Windows 10 Dark Mode for application title bars
    /// </summary>
    public static class DarkTitleBar
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int TRUE = 1;
        
        // Window message identifiers for caption color
        private const uint WM_NCPAINT = 0x0085;
        
        /// <summary>
        /// Applies dark mode to the title bar of the specified form
        /// </summary>
        /// <param name="form">The form to apply dark mode to</param>
        public static void ApplyDarkTitle(Form form)
        {
            if (Environment.OSVersion.Version.Major >= 10)
            {
                int darkMode = TRUE;
                DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
                
                // Refresh the form
                SendMessage(form.Handle, WM_NCPAINT, 0, 0);
            }
        }
    }
} 