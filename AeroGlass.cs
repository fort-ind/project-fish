using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace fortindwindows
{
    /// <summary>
    /// steals your aero glass
    /// https://learn.microsoft.com/windows/win32/dwm/customframe
    /// </summary>
    internal static class AeroGlass
    {
        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// True if DWM composition (Aero Glass) is currently active on the desktop.
        /// False on non-Aero SKUs (e.g. Windows 7 Starter/Home Basic), when the user
        /// has selected a Basic/Classic theme, over most RDP sessions, etc.
        /// </summary>
        public static bool IsCompositionEnabled()
        {
            try
            {
                return DwmIsCompositionEnabled();
            }
            catch (DllNotFoundException)
            {
                // dwmapi.dll not present (pre-Vista). Should not? happen on Windows 7-
                return false;
            }
            catch (EntryPointNotFoundException)
            {
                return false;
            }
        }

        
        public static bool ExtendFrame(Form form, int left, int top, int right, int bottom)
        {
            if (form == null || !form.IsHandleCreated)
                return false;

            if (!IsCompositionEnabled())
                return false;

            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = left;
            margins.cxRightWidth = right;
            margins.cyTopHeight = top;
            margins.cyBottomHeight = bottom;

            try
            {
                DwmExtendFrameIntoClientArea(form.Handle, ref margins);
                return true;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
        }
    }
}
