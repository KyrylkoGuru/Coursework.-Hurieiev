namespace ProjectAurora
{
    partial class MainHall
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
            this.HallPic = new System.Windows.Forms.PictureBox();
            this.buttonToPlane = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.HallPic)).BeginInit();
            this.SuspendLayout();
            // 
            // HallPic
            // 
            this.HallPic.BackColor = System.Drawing.Color.Transparent;
            this.HallPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HallPic.Image = global::ProjectAurora.Properties.Resources.map_png;
            this.HallPic.Location = new System.Drawing.Point(0, 0);
            this.HallPic.Name = "HallPic";
            this.HallPic.Size = new System.Drawing.Size(829, 458);
            this.HallPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HallPic.TabIndex = 0;
            this.HallPic.TabStop = false;
            this.HallPic.Resize += new System.EventHandler(this.HallPic_Resize);
            // 
            // buttonToPlane
            // 
            this.buttonToPlane.Location = new System.Drawing.Point(54, 24);
            this.buttonToPlane.Name = "buttonToPlane";
            this.buttonToPlane.Size = new System.Drawing.Size(101, 23);
            this.buttonToPlane.TabIndex = 1;
            this.buttonToPlane.Text = "To Plane";
            this.buttonToPlane.UseVisualStyleBackColor = true;
            this.buttonToPlane.Click += new System.EventHandler(this.buttonToPlane_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(683, 24);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(89, 23);
            this.buttonInfo.TabIndex = 2;
            this.buttonInfo.Text = "Information";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(710, 411);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // MainHall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 458);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonToPlane);
            this.Controls.Add(this.HallPic);
            this.Name = "MainHall";
            this.Text = "Main Hall";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HallPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HallPic;
        private System.Windows.Forms.Button buttonToPlane;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonExit;
    }
}

