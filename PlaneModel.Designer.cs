namespace ProjectAurora
{
    partial class PlaneModel
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tlpFirstClass = new System.Windows.Forms.TableLayoutPanel();
            this.tlpEconomyClass = new System.Windows.Forms.TableLayoutPanel();
            this.buttonBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ProjectAurora.Properties.Resources.Plane_1_png;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(783, 503);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // tlpFirstClass
            // 
            this.tlpFirstClass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpFirstClass.BackColor = System.Drawing.Color.Transparent;
            this.tlpFirstClass.ColumnCount = 2;
            this.tlpFirstClass.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFirstClass.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFirstClass.Location = new System.Drawing.Point(281, 12);
            this.tlpFirstClass.Name = "tlpFirstClass";
            this.tlpFirstClass.RowCount = 2;
            this.tlpFirstClass.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.33333F));
            this.tlpFirstClass.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.66667F));
            this.tlpFirstClass.Size = new System.Drawing.Size(221, 109);
            this.tlpFirstClass.TabIndex = 1;
            // 
            // tlpEconomyClass
            // 
            this.tlpEconomyClass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEconomyClass.AutoSize = true;
            this.tlpEconomyClass.BackColor = System.Drawing.Color.Transparent;
            this.tlpEconomyClass.ColumnCount = 2;
            this.tlpEconomyClass.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.93617F));
            this.tlpEconomyClass.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.06383F));
            this.tlpEconomyClass.Location = new System.Drawing.Point(281, 127);
            this.tlpEconomyClass.Name = "tlpEconomyClass";
            this.tlpEconomyClass.RowCount = 2;
            this.tlpEconomyClass.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.69565F));
            this.tlpEconomyClass.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.30435F));
            this.tlpEconomyClass.Size = new System.Drawing.Size(221, 352);
            this.tlpEconomyClass.TabIndex = 0;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(677, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "To main page";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // PlaneModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 503);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.tlpEconomyClass);
            this.Controls.Add(this.tlpFirstClass);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PlaneModel";
            this.Text = "Airplane Cabin";
            this.Load += new System.EventHandler(this.PlaneModel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tlpFirstClass;
        private System.Windows.Forms.TableLayoutPanel tlpEconomyClass;
        private System.Windows.Forms.Button buttonBack;
    }
}