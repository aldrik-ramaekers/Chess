using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum BoardTileColor
    {
        White,
        Black,
    }

    public class BoardTile
    {
        public int X;
        public int Y;
        public BoardTileColor Color;
        public ChessPiece OccupyingPiece;
        https://git.fhict.nl/I436104/chess
        public BoardTile(int x, int y, BoardTileColor color)
        {
            X = x;
            Y = y;
            Color = color;
            OccupyingPiece = null;
        }

        internal void Draw(Graphics graphics, float tileWidth, float tileHeight)
        {
            graphics.FillRectangle(new SolidBrush(this.Color == BoardTileColor.White ? System.Drawing.Color.White : System.Drawing.Color.Black), new RectangleF(X * tileWidth, Y * tileHeight, tileWidth, tileHeight));

            OccupyingPiece?.Draw(graphics, X, Y, tileWidth, tileHeight);
        }
    }
}
