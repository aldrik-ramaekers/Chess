using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class MainForm : Form
    {
        private ChessBoard board;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FitBoardContainerToScreen()
        {
            this.chessBoardBitmap.Location = new Point(0, 0);
            this.chessBoardBitmap.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FitBoardContainerToScreen();
            this.board = new ChessBoard(this.chessBoardBitmap);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FitBoardContainerToScreen();
            this.board.HandleResize();
        }
    }
}
