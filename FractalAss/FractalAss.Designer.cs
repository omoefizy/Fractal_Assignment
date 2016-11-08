namespace FractalAss
{
    partial class FractalAss
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
            this.omoboBox = new System.Windows.Forms.TextBox();
            this.djeasyPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.djeasyPic)).BeginInit();
            this.SuspendLayout();
            // 
            // omoboBox
            // 
            this.omoboBox.Location = new System.Drawing.Point(-1, 399);
            this.omoboBox.Name = "omoboBox";
            this.omoboBox.Size = new System.Drawing.Size(569, 20);
            this.omoboBox.TabIndex = 0;
                   // 
            // djeasyPic
            // 
            this.djeasyPic.Location = new System.Drawing.Point(-1, 2);
            this.djeasyPic.Name = "djeasyPic";
            this.djeasyPic.Size = new System.Drawing.Size(569, 391);
            this.djeasyPic.TabIndex = 1;
            this.djeasyPic.TabStop = false;
            
            // 
            // FractalAss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 420);
            this.Controls.Add(this.djeasyPic);
            this.Controls.Add(this.omoboBox);
            this.Name = "FractalAss";
            this.Text = "Fractal ";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FractalAss_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FractalAss_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FractalAss_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.djeasyPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox omoboBox;
        private System.Windows.Forms.PictureBox djeasyPic;
    }
}

