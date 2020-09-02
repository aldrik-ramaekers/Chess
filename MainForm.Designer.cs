namespace Chess
{
    partial class MainForm
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
            this.chessBoardBitmap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chessBoardBitmap)).BeginInit();
            this.SuspendLayout();
            // 
            // chessBoardBitmap
            // 
            this.chessBoardBitmap.Location = new System.Drawing.Point(254, 61);
            this.chessBoardBitmap.Name = "chessBoardBitmap";
            this.chessBoardBitmap.Size = new System.Drawing.Size(173, 146);
            this.chessBoardBitmap.TabIndex = 0;
            this.chessBoardBitmap.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.chessBoardBitmap);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "MainForm";
            this.Text = "Chess Xtreme";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chessBoardBitmap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox chessBoardBitmap;
    }
}

