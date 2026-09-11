
namespace Mega_World_Mall_Linking.Views
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Progress = new Bunifu.UI.WinForms.BunifuProgressBar();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.lbl_date = new Bunifu.UI.WinForms.BunifuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Mega_World_Mall_Linking.Properties.Resources.giphy;
            this.pictureBox1.Location = new System.Drawing.Point(124, -41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(311, 293);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Progress
            // 
            this.Progress.AllowAnimations = false;
            this.Progress.Animation = 0;
            this.Progress.AnimationSpeed = 220;
            this.Progress.AnimationStep = 10;
            this.Progress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Progress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Progress.BackgroundImage")));
            this.Progress.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Progress.BorderRadius = 9;
            this.Progress.BorderThickness = 1;
            this.Progress.Location = new System.Drawing.Point(124, 347);
            this.Progress.Maximum = 100;
            this.Progress.MaximumValue = 100;
            this.Progress.Minimum = 0;
            this.Progress.MinimumValue = 0;
            this.Progress.Name = "Progress";
            this.Progress.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.Progress.ProgressBackColor = System.Drawing.Color.WhiteSmoke;
            this.Progress.ProgressColorLeft = System.Drawing.Color.AliceBlue;
            this.Progress.ProgressColorRight = System.Drawing.Color.AliceBlue;
            this.Progress.Size = new System.Drawing.Size(311, 39);
            this.Progress.TabIndex = 1;
            this.Progress.Value = 50;
            this.Progress.ValueByTransition = 50;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuLabel1.Location = new System.Drawing.Point(196, 258);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(167, 38);
            this.bunifuLabel1.TabIndex = 2;
            this.bunifuLabel1.Text = "LOADING...";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // lbl_date
            // 
            this.lbl_date.AllowParentOverrides = false;
            this.lbl_date.AutoEllipsis = false;
            this.lbl_date.CursorType = null;
            this.lbl_date.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbl_date.ForeColor = System.Drawing.Color.White;
            this.lbl_date.Location = new System.Drawing.Point(124, 302);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_date.Size = new System.Drawing.Size(95, 38);
            this.lbl_date.TabIndex = 3;
            this.lbl_date.Text = "(DATE)";
            this.lbl_date.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_date.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(549, 414);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Megaworld Mall Linking";
            this.Load += new System.EventHandler(this.Loading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.UI.WinForms.BunifuProgressBar Progress;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuLabel lbl_date;
    }
}