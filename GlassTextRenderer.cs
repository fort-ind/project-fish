using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace fortindwindows
{
    /// <summary>
    /// i stole the code from
    /// https://learn.microsoft.com/windows/win32/dwm/customframe
    /// </summary>
    internal static class GlassTextRenderer
    {
        public static void DrawGlowText(Graphics destination, Rectangle bounds, string text, Font font, Color color)
        {
            DrawGlowText(destination, bounds, text, font, color, 8);
        }

        public static void DrawGlowText(Graphics destination, Rectangle bounds, string text, Font font, Color color, int glowSize)
        {
            if (destination == null || font == null || string.IsNullOrEmpty(text))
                return;
            if (bounds.Width <= 0 || bounds.Height <= 0)
                return;

            IntPtr hTheme = NativeMethods.OpenThemeData(IntPtr.Zero, "CompositedWindow::Window");
            if (hTheme == IntPtr.Zero)
            {
                DrawPlainFallback(destination, bounds, text, font, color);
                return;
            }

            try
            {
                DrawComposited(destination, bounds, text, font, color, glowSize, hTheme);
            }
            finally
            {
                NativeMethods.CloseThemeData(hTheme);
            }
        }

        private static void DrawComposited(Graphics destination, Rectangle bounds, string text, Font font, Color color, int glowSize, IntPtr hTheme)
        {
            // Build the off-screen composited-text DIB against the screen (a NULL
            // hdc),
            IntPtr memDc = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            if (memDc == IntPtr.Zero)
            {
                DrawPlainFallback(destination, bounds, text, font, color);
                return;
            }

            try
            {
                NativeMethods.BITMAPINFOHEADER bmi = new NativeMethods.BITMAPINFOHEADER();
                bmi.biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER));
                bmi.biWidth = bounds.Width;
                bmi.biHeight = -bounds.Height;
                bmi.biPlanes = 1;
                bmi.biBitCount = 32;
                bmi.biCompression = 0; // BI_RGB

                IntPtr bits;
                IntPtr hBitmap = NativeMethods.CreateDIBSection(IntPtr.Zero, ref bmi, 0, out bits, IntPtr.Zero, 0);
                if (hBitmap == IntPtr.Zero)
                {
                    DrawPlainFallback(destination, bounds, text, font, color);
                    return;
                }

                try
                {
                    IntPtr oldBitmap = NativeMethods.SelectObject(memDc, hBitmap);
                    IntPtr hFont = font.ToHfont();
                    IntPtr oldFont = NativeMethods.SelectObject(memDc, hFont);

                    try
                    {
                        NativeMethods.DTTOPTS options = new NativeMethods.DTTOPTS();
                        options.dwSize = Marshal.SizeOf(typeof(NativeMethods.DTTOPTS));
                        options.dwFlags = NativeMethods.DTT_COMPOSITED | NativeMethods.DTT_GLOWSIZE | NativeMethods.DTT_TEXTCOLOR;
                        options.crText = ColorTranslator.ToWin32(color);
                        options.iGlowSize = glowSize;

                        NativeMethods.RECT rect = new NativeMethods.RECT();
                        rect.Left = 0;
                        rect.Top = 0;
                        rect.Right = bounds.Width;
                        rect.Bottom = bounds.Height;

                        uint dtFlags = NativeMethods.DT_LEFT | NativeMethods.DT_VCENTER |
                                       NativeMethods.DT_SINGLELINE | NativeMethods.DT_NOPREFIX |
                                       NativeMethods.DT_END_ELLIPSIS;

                        NativeMethods.DrawThemeTextEx(hTheme, memDc, 0, 0, text, text.Length, dtFlags, ref rect, ref options);

                        
                        NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION();
                        blend.BlendOp = NativeMethods.AC_SRC_OVER;
                        blend.BlendFlags = 0;
                        blend.SourceConstantAlpha = 255;
                        blend.AlphaFormat = NativeMethods.AC_SRC_ALPHA;

                        IntPtr destHdc = destination.GetHdc();
                        try
                        {
                            NativeMethods.AlphaBlend(destHdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height,
                                memDc, 0, 0, bounds.Width, bounds.Height, blend);
                        }
                        finally
                        {
                            destination.ReleaseHdc(destHdc);
                        }
                    }
                    finally
                    {
                        NativeMethods.SelectObject(memDc, oldFont);
                        NativeMethods.DeleteObject(hFont);
                        NativeMethods.SelectObject(memDc, oldBitmap);
                    }
                }
                finally
                {
                    NativeMethods.DeleteObject(hBitmap);
                }
            }
            finally
            {
                NativeMethods.DeleteDC(memDc);
            }
        }

        private static void DrawPlainFallback(Graphics destination, Rectangle bounds, string text, Font font, Color color)
        {
            // No theming available (classic theme / composition off) - plain GDI text :(
            TextRenderer.DrawText(destination, text, font, bounds, color,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        private static class NativeMethods
        {
            public const uint DT_LEFT = 0x00000000;
            public const uint DT_VCENTER = 0x00000004;
            public const uint DT_SINGLELINE = 0x00000020;
            public const uint DT_NOPREFIX = 0x00000800;
            public const uint DT_END_ELLIPSIS = 0x00008000;

            public const uint DTT_TEXTCOLOR = 0x00000001;
            public const uint DTT_GLOWSIZE = 0x00000800;
            public const uint DTT_COMPOSITED = 0x00002000;

            public const int SRCCOPY = 0x00CC0020;

            public const byte AC_SRC_OVER = 0x00;
            public const byte AC_SRC_ALPHA = 0x01;

            [StructLayout(LayoutKind.Sequential)]
            public struct BLENDFUNCTION
            {
                public byte BlendOp;
                public byte BlendFlags;
                public byte SourceConstantAlpha;
                public byte AlphaFormat;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct BITMAPINFOHEADER
            {
                public int biSize;
                public int biWidth;
                public int biHeight;
                public short biPlanes;
                public short biBitCount;
                public int biCompression;
                public int biSizeImage;
                public int biXPelsPerMeter;
                public int biYPelsPerMeter;
                public int biClrUsed;
                public int biClrImportant;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct DTTOPTS
            {
                public int dwSize;
                public uint dwFlags;
                public int crText;
                public int crBorder;
                public int crShadow;
                public int iTextShadowType;
                public int ptShadowOffsetX;
                public int ptShadowOffsetY;
                public int iBorderSize;
                public int iFontPropId;
                public int iColorPropId;
                public int iStateId;
                public int fApplyOverlay;
                public int iGlowSize;
                public IntPtr pfnDrawTextCallback;
                public IntPtr lParam;
            }

            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);

            [DllImport("uxtheme.dll")]
            public static extern int CloseThemeData(IntPtr hTheme);

            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
            public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId,
                string text, int iCharCount, uint dwFlags, ref RECT rect, ref DTTOPTS options);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFOHEADER pbmi, uint usage,
                out IntPtr ppvBits, IntPtr hSection, uint offset);

            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height,
                IntPtr hdcSrc, int xSrc, int ySrc, int rop);

            [DllImport("msimg32.dll")]
            public static extern bool AlphaBlend(IntPtr hdcDest, int xDest, int yDest, int widthDest, int heightDest,
                IntPtr hdcSrc, int xSrc, int ySrc, int widthSrc, int heightSrc, BLENDFUNCTION blendFunction);
        }
    }
}
