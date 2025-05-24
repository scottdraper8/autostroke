using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoStroke
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                // Enable smooth font rendering with a modern font
                Application.SetDefaultFont(new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point));
                
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application Error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                               "AutoStroke Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 