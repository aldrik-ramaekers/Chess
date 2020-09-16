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
        private Game game;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FitBoardContainerToScreen()
        {
            this.columnHeader1.Width = 30;

            this.chessBoardBitmap.Location = new Point(0, 0);
            this.chessBoardBitmap.Size = new Size(this.ClientSize.Width - this.gameControlPanel.Width, this.ClientSize.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FitBoardContainerToScreen();
            this.game = new Game(this);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            var form = sender as MainForm;
            if (form.WindowState == FormWindowState.Minimized) return;

            FitBoardContainerToScreen();
            this.game.HandleResize();
        }

        private void chessBoardBitmap_MouseDown(object sender, MouseEventArgs e)
        {
            this.game.HandleClick(e.X, e.Y);
        }
    }
}
