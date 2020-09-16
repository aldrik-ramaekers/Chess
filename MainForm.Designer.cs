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
            this.gameControlPanel = new System.Windows.Forms.Panel();
            this.moveList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.chessBoardBitmap)).BeginInit();
            this.gameControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // chessBoardBitmap
            // 
            this.chessBoardBitmap.Location = new System.Drawing.Point(254, 61);
            this.chessBoardBitmap.Name = "chessBoardBitmap";
            this.chessBoardBitmap.Size = new System.Drawing.Size(173, 146);
            this.chessBoardBitmap.TabIndex = 0;
            this.chessBoardBitmap.TabStop = false;
            this.chessBoardBitmap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chessBoardBitmap_MouseDown);
            // 
            // gameControlPanel
            // 
            this.gameControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameControlPanel.Controls.Add(this.moveList);
            this.gameControlPanel.Location = new System.Drawing.Point(685, -1);
            this.gameControlPanel.Name = "gameControlPanel";
            this.gameControlPanel.Size = new System.Drawing.Size(200, 661);
            this.gameControlPanel.TabIndex = 1;
            // 
            // moveList
            // 
            this.moveList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moveList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.moveList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.moveList.HideSelection = false;
            this.moveList.Location = new System.Drawing.Point(5, 5);
            this.moveList.MultiSelect = false;
            this.moveList.Name = "moveList";
            this.moveList.ShowGroups = false;
            this.moveList.Size = new System.Drawing.Size(190, 390);
            this.moveList.TabIndex = 0;
            this.moveList.UseCompatibleStateImageBehavior = false;
            this.moveList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 67;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 58;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.gameControlPanel);
            this.Controls.Add(this.chessBoardBitmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 700);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Chess Xtreme";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chessBoardBitmap)).EndInit();
            this.gameControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel gameControlPanel;
        public System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public System.Windows.Forms.PictureBox chessBoardBitmap;
        public System.Windows.Forms.ListView moveList;
    }
}

