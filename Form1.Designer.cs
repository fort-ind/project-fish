
namespace fortindwindows
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.home = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.profile = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.games = new System.Windows.Forms.TabPage();
            this.panelGlassNav = new System.Windows.Forms.Panel();
            this.tabs.SuspendLayout();
            this.home.SuspendLayout();
            this.profile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.home);
            this.tabs.Controls.Add(this.profile);
            this.tabs.Controls.Add(this.games);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 46);
            this.tabs.Margin = new System.Windows.Forms.Padding(2);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(600, 320);
            this.tabs.TabIndex = 0;
            // 
            // home
            // 
            this.home.AccessibleDescription = "the home menu of fort.desktop legacy";
            this.home.AccessibleName = "home menu";
            this.home.Controls.Add(this.label1);
            this.home.Location = new System.Drawing.Point(4, 22);
            this.home.Margin = new System.Windows.Forms.Padding(2);
            this.home.Name = "home";
            this.home.Padding = new System.Windows.Forms.Padding(2);
            this.home.Size = new System.Drawing.Size(592, 294);
            this.home.TabIndex = 0;
            this.home.Text = "home";
            this.home.UseVisualStyleBackColor = true;
            this.home.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "oh uh hi :3";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // profile
            // 
            this.profile.AccessibleDescription = "sign in sign out go wild!";
            this.profile.AccessibleName = "profile";
            this.profile.Controls.Add(this.button1);
            this.profile.Controls.Add(this.label8);
            this.profile.Controls.Add(this.label7);
            this.profile.Controls.Add(this.label6);
            this.profile.Controls.Add(this.label5);
            this.profile.Location = new System.Drawing.Point(4, 22);
            this.profile.Margin = new System.Windows.Forms.Padding(2);
            this.profile.Name = "profile";
            this.profile.Padding = new System.Windows.Forms.Padding(2);
            this.profile.Size = new System.Drawing.Size(592, 294);
            this.profile.TabIndex = 1;
            this.profile.Text = "profile";
            this.profile.UseVisualStyleBackColor = true;
            this.profile.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "sign in!";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(404, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "and more...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(374, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "customize your profile!";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(349, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "sync your profile across fort.social";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(310, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sign in to your fort.ind account!";
            // 
            // games
            // 
            this.games.AccessibleDescription = "view games on fort.ind and play them";
            this.games.AccessibleName = "games";
            this.games.Location = new System.Drawing.Point(4, 22);
            this.games.Name = "games";
            this.games.Padding = new System.Windows.Forms.Padding(3);
            this.games.Size = new System.Drawing.Size(592, 294);
            this.games.TabIndex = 2;
            this.games.Text = "games";
            this.games.UseVisualStyleBackColor = true;
            // 
            // panelGlassNav
            // 
            this.panelGlassNav.BackColor = System.Drawing.Color.Black;
            this.panelGlassNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGlassNav.Location = new System.Drawing.Point(0, 0);
            this.panelGlassNav.Margin = new System.Windows.Forms.Padding(2);
            this.panelGlassNav.Name = "panelGlassNav";
            this.panelGlassNav.Size = new System.Drawing.Size(600, 46);
            this.panelGlassNav.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.panelGlassNav);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "fort.ind (legacy)";
            this.tabs.ResumeLayout(false);
            this.home.ResumeLayout(false);
            this.home.PerformLayout();
            this.profile.ResumeLayout(false);
            this.profile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage home;
        private System.Windows.Forms.TabPage profile;
        private System.Windows.Forms.Panel panelGlassNav;
        private System.Windows.Forms.TabPage games;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}

