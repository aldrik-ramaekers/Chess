using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public class ChessBoard
    {
        const int boardSize = 8;

        private float tileWidth;
        private float tileHeight;

        private BoardTile[,] tiles;
        private PictureBox container;

        public ChessBoard(PictureBox container)
        {
            this.container = container;

            GenerateBoard();
            GeneratePieces();

            DrawGame();
        }

        public void HandleResize()
        {
            DrawGame();
        }

        public void GeneratePieces()
        {
            for (int x = 0; x < boardSize; x++)
                tiles[1, x].OccupyingPiece = new Pawn();

            for (int x = 0; x < boardSize; x++)
                tiles[6, x].OccupyingPiece = new Pawn();
        }

        public void GenerateBoard()
        {
            tiles = new BoardTile[boardSize, boardSize];

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    BoardTileColor color = BoardTileColor.White;

                    if (y % 2 == 0)
                    {
                        if (x % 2 != 0) color = BoardTileColor.Black;
                    }
                    else
                    {
                        if (x % 2 == 0) color = BoardTileColor.Black;
                    }

                    tiles[y, x] = new BoardTile(x, y, color);
                }
            }
        }

        public void DrawGame()
        {
            Bitmap temp = new Bitmap(container.Size.Width, container.Size.Height);

            tileWidth = container.Size.Width / (float)boardSize;
            tileHeight = container.Size.Height / (float)boardSize;

            using (Graphics g = Graphics.FromImage(temp))
            {
                for (int y = 0; y < boardSize; y++)
                {
                    for (int x = 0; x < boardSize; x++)
                    {
                        BoardTile tile = tiles[y, x];

                        tile.Draw(g, tileWidth, tileHeight);
                    }
                }
            }

            if (container.Image != null)
                container.Image.Dispose();

            container.Image = temp;
        }
    }
}
