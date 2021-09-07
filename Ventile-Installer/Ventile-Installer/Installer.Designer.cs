
namespace Ventile_Installer
{
    partial class Installer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Installer));
            this.topPanel = new System.Windows.Forms.Panel();
            this.minus = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.fadeIn = new System.Windows.Forms.Timer(this.components);
            this.fadeOut = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.versionSelector = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedRelease = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.topPanel.Controls.Add(this.minus);
            this.topPanel.Controls.Add(this.close);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(800, 27);
            this.topPanel.TabIndex = 2;
            // 
            // minus
            // 
            this.minus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minus.BackColor = System.Drawing.Color.Transparent;
            this.minus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.minus.FlatAppearance.BorderSize = 0;
            this.minus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.minus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minus.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold);
            this.minus.ForeColor = System.Drawing.Color.Silver;
            this.minus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.minus.Location = new System.Drawing.Point(743, -13);
            this.minus.Margin = new System.Windows.Forms.Padding(0);
            this.minus.Name = "minus";
            this.minus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.minus.Size = new System.Drawing.Size(29, 52);
            this.minus.TabIndex = 0;
            this.minus.Text = "-";
            this.minus.UseCompatibleTextRendering = true;
            this.minus.UseVisualStyleBackColor = false;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // close
            // 
            this.close.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.close.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Font = new System.Drawing.Font("Calibri", 13.75F, System.Drawing.FontStyle.Bold);
            this.close.ForeColor = System.Drawing.Color.Silver;
            this.close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.close.Location = new System.Drawing.Point(767, -8);
            this.close.Margin = new System.Windows.Forms.Padding(0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 43);
            this.close.TabIndex = 0;
            this.close.TabStop = false;
            this.close.Text = "X";
            this.close.UseCompatibleTextRendering = true;
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // fadeIn
            // 
            this.fadeIn.Enabled = true;
            this.fadeIn.Interval = 1;
            this.fadeIn.Tick += new System.EventHandler(this.FadeIn_Tick);
            // 
            // fadeOut
            // 
            this.fadeOut.Interval = 1;
            this.fadeOut.Tick += new System.EventHandler(this.FadeOut_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ventile_Installer.Properties.Resources.Label;
            this.pictureBox1.Location = new System.Drawing.Point(175, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.button1.ContextMenuStrip = this.versionSelector;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button1.FlatAppearance.BorderSize = 4;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(272, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 64);
            this.button1.TabIndex = 4;
            this.button1.Text = "Install";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // versionSelector
            // 
            this.versionSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.versionSelector.Name = "versionSelector";
            this.versionSelector.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.versionSelector.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.versionSelector.RenderStyle.ColorTable = null;
            this.versionSelector.RenderStyle.RoundedEdges = true;
            this.versionSelector.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.versionSelector.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.versionSelector.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.versionSelector.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.versionSelector.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.versionSelector.Size = new System.Drawing.Size(61, 4);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.button2.FlatAppearance.BorderSize = 4;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(272, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(245, 64);
            this.button2.TabIndex = 5;
            this.button2.Text = "Uninstall";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(329, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Installing...";
            // 
            // selectedRelease
            // 
            this.selectedRelease.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedRelease.ForeColor = System.Drawing.Color.White;
            this.selectedRelease.Location = new System.Drawing.Point(272, 337);
            this.selectedRelease.Name = "selectedRelease";
            this.selectedRelease.Size = new System.Drawing.Size(245, 35);
            this.selectedRelease.TabIndex = 7;
            this.selectedRelease.Text = "SelectedVersion";
            this.selectedRelease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.topPanel;
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectedRelease);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Installer";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Installer_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Timer fadeIn;
        private System.Windows.Forms.Timer fadeOut;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label selectedRelease;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip versionSelector;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}

