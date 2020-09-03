using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        public BoardTile(int x, int y)
        {
            X = x;
            Y = y;

            Color = BoardTileColor.White;
            if (y % 2 == 0)
            {
                if (x % 2 != 0) Color = BoardTileColor.Black;
            }
            else
            {
                if (x % 2 == 0) Color = BoardTileColor.Black;
            }

            OccupyingPiece = null;
        }

        public bool CanMoveTo(ChessBoard board, BoardTile tile)
        {
            if (OccupyingPiece != null) return OccupyingPiece.CanMoveTo(board, this, tile);

            return false;
        }

        internal void Draw(Graphics graphics, float tileWidth, float tileHeight, bool active)
        {
            graphics.FillRectangle(new SolidBrush(this.Color == BoardTileColor.White ? System.Drawing.Color.FromArgb(235, 236, 208) : System.Drawing.Color.FromArgb(137, 163, 105)), 
                new RectangleF(X * tileWidth, Y * tileHeight, tileWidth, tileHeight));

            OccupyingPiece?.Draw(graphics, X, Y, tileWidth, tileHeight);

            if (active)
            {
                float marginw = tileWidth / 3;
                float marginh = tileHeight / 3;
                int padding = 3;
                graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(150, 50, 50, 50)), X * tileWidth + marginw, Y * tileHeight + marginh, tileWidth - marginw * 2, tileHeight - marginh * 2);
                graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(150, 255, 200, 200)), X * tileWidth + marginw + padding, Y * tileHeight + marginh + padding, tileWidth - (marginw + padding) * 2, tileHeight - (marginh + padding) * 2);
            }
        }
    }
}
