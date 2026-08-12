using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fortindwindows
{
    public partial class Form1 : Form
    {
        private const string TitleText = "fort.ind";

        private readonly List<GlassNavButton> _navButtons = new List<GlassNavButton>();
        private bool _glassActive;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            BuildNavStrip();

            tabs.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            panelGlassNav.Paint += PanelGlassNav_Paint;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyGlass();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == AeroGlass.WM_DWMCOMPOSITIONCHANGED)
            {
                ApplyGlass();
            }
        }

        private void ApplyGlass()
        {
            _glassActive = AeroGlass.ExtendFrame(this, 0, panelGlassNav.Height, 0, 0);

            panelGlassNav.BackColor = _glassActive ? Color.Black : SystemColors.Control;

            foreach (GlassNavButton button in _navButtons)
            {
                button.GlassMode = _glassActive;
            }

            panelGlassNav.Invalidate(true);
        }

        /// <summary>
        /// Builds one nav button per existing TabPage
        /// </summary>
        private void BuildNavStrip()
        {
            const int buttonHeight = 32;
            const int buttonWidth = 110;
            const int gap = 6;
            int x = 170; 
            int y = (panelGlassNav.Height - buttonHeight) / 2;

            foreach (TabPage page in tabs.TabPages)
            {
                TabPage capturedPage = page;

                GlassNavButton button = new GlassNavButton();
                button.Text = page.Text;
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(x, y);
                button.Selected = tabs.SelectedTab == page;
                button.Click += delegate { tabs.SelectedTab = capturedPage; };

                panelGlassNav.Controls.Add(button);
                _navButtons.Add(button);

                x += buttonWidth + gap;
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _navButtons.Count && i < tabs.TabPages.Count; i++)
            {
                _navButtons[i].Selected = tabs.TabPages[i] == tabs.SelectedTab;
            }
        }

        private void PanelGlassNav_Paint(object sender, PaintEventArgs e)
        {
            Rectangle titleRect = new Rectangle(16, 0, 145, panelGlassNav.Height);

            // Semibold + no glow, same reasoning as GlassNavButton: at UI text
            // sizes (even 13pt) DTT_GLOWSIZE blurs the letterforms rather than
            // haloing them, and it's now an explicit "0 means 0" call rather than
            // the old default-glow overload.
            using (Font titleFont = new Font("Segoe UI", 13f, FontStyle.Bold))
            {
                if (_glassActive)
                {
                    GlassTextRenderer.DrawGlowText(e.Graphics, titleRect, TitleText, titleFont, Color.White, 0);
                }
                else
                {
                    TextRenderer.DrawText(e.Graphics, TitleText, titleFont, titleRect, SystemColors.ControlText,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
