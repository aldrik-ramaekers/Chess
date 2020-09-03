using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        internal BoardTile[,] tiles;
        private PictureBox container;

        private BoardTile selectedTile = null;

        private bool screenInvalidated = true;

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
            tiles[0, 0].OccupyingPiece = new Rook(false);
            tiles[0, 1].OccupyingPiece = new Knight(false);
            tiles[0, 2].OccupyingPiece = new Bishop(false);
            tiles[4,4].OccupyingPiece = new Queen(false);
            tiles[0, 4].OccupyingPiece = new King(false);
            tiles[0, 5].OccupyingPiece = new Bishop(false);
            tiles[0, 6].OccupyingPiece = new Knight(false);
            tiles[0, 7].OccupyingPiece = new Rook(false);

            for (int x = 0; x < boardSize; x++)
                tiles[1, x].OccupyingPiece = new Pawn(false);

            for (int x = 0; x < boardSize; x++)
                tiles[6, x].OccupyingPiece = new Pawn(true);

            tiles[7, 0].OccupyingPiece = new Rook(true);
            tiles[7, 1].OccupyingPiece = new Knight(true);
            tiles[7, 2].OccupyingPiece = new Bishop(true);
            tiles[4,5].OccupyingPiece = new Queen(true);
            tiles[3, 3].OccupyingPiece = new King(true);
            tiles[7, 5].OccupyingPiece = new Bishop(true);
            tiles[7, 6].OccupyingPiece = new Knight(true);
            tiles[7, 7].OccupyingPiece = new Rook(true);
        }

        public ChessPiece PieceAt(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < boardSize && y < boardSize)
            {
                return tiles[y, x].OccupyingPiece;
            }

            return null;
        }

        public void GenerateBoard()
        {
            tiles = new BoardTile[boardSize, boardSize];

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    tiles[y, x] = new BoardTile(x, y);
                }
            }
        }

        public void DrawTile(Point point)
        {
            if (point.X >= 0 && point.X < boardSize && point.Y >= 0 && point.Y < boardSize)
            {
                var tile = tiles[point.Y, point.X];

                using (Graphics g = (screenInvalidated ? Graphics.FromImage(container.Image) : container.CreateGraphics()))
                {
                    tile.Draw(g, tileWidth, tileHeight, (selectedTile != null && selectedTile.CanMoveTo(this, tile)));
                }
            }
        }

        public void DrawGame()
        {
            screenInvalidated = true;

            if (container.Image != null)
                container.Image.Dispose();

            container.Image = new Bitmap(container.Size.Width, container.Size.Height);

            tileWidth = container.Size.Width / (float)boardSize;
            tileHeight = container.Size.Height / (float)boardSize;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    DrawTile(new Point(x, y));
                }
            }

            screenInvalidated = false;
        }

        public Point MouseToTilePosition(int x, int y)
        {
            return new Point((int)(x / tileWidth), (int)(y / tileHeight));
        }

        private void MoveSelectedPieceTo(BoardTile tile)
        {
            if (selectedTile.CanMoveTo(this, tile))
            {
                tile.OccupyingPiece = selectedTile.OccupyingPiece;
                selectedTile.OccupyingPiece = null;
            }
        }

        public void SelectTile(Point point)
        {
            var clickedTile = tiles[point.Y, point.X];

            if (selectedTile != clickedTile)
            {
                if (selectedTile != null)
                {
                    MoveSelectedPieceTo(clickedTile);
                }
               
                selectedTile = clickedTile;
            }
            else
            {
                selectedTile = null;
            }

            DrawGame();
        }
    }
}
