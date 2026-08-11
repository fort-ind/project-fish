using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace fortindwindows
{
    /// <summary>
    /// aero glass weeee
    /// </summary>
    internal class GlassNavButton : Control
    {
        private bool _hot;
        private bool _selected;
        private bool _glassMode;

        public GlassNavButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            _glassMode = true;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 9.5f);
            TabStop = true;
        }

        public bool GlassMode
        {
            get { return _glassMode; }
            set
            {
                if (_glassMode != value)
                {
                    _glassMode = value;
                    Invalidate();
                }
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _hot = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hot = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, Width, Height);

            if (_glassMode)
            {
                // transparent, so the glass behind this control shows through.
                g.Clear(Color.Black);
            }
            else
            {
                Color fallback = Parent != null ? Parent.BackColor : SystemColors.Control;
                g.Clear(fallback);
            }

            if (_hot || _selected)
            {
                using (GraphicsPath path = RoundedRect(bounds, 3))
                {
                    int alpha = _selected ? 90 : 55;
                    Color highlight = _glassMode ? Color.White : SystemColors.ControlLight;
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, highlight)))
                    {
                        g.FillPath(brush, path);
                    }
                }
            }

            Rectangle textRect = Rectangle.Inflate(bounds, -10, 0);
            if (_glassMode)
            {
                GlassTextRenderer.DrawGlowText(g, textRect, Text, Font, Color.White);
            }
            else
            {
                TextRenderer.DrawText(g, Text, Font, textRect, SystemColors.ControlText,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
