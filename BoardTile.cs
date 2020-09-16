using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    [Flags]
    public enum TileState 
    {
        /// <summary>
        /// Default state.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Selected by user.
        /// </summary>
        Selected = 2,

        /// <summary>
        /// Possible target for selected piece.
        /// </summary>
        Targeted = 4,

        /// <summary>
        /// Involved in latest piece movement.
        /// </summary>
        Latest = 8,
    }

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
        public TileState State;

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

            State = TileState.Normal;
            OccupyingPiece = null;
        }

        public override string ToString()
        {
            return (((char)('a' + (char)X)).ToString() + (ChessBoard.BoardSize - Y).ToString()).ToString().ToUpper();
        }

        public bool CanMoveTo(ChessBoard board, BoardTile tile)
        {
            // By putting this code here we check whether or not the king will be attacked if we move,
            // and also checks what spots we CAN move to when the king is under attack.
            if (OccupyingPiece.WillKingBeAttackedIfMovedTo(board, this, tile))
            {
                return false;
            }

            if (OccupyingPiece != null) return OccupyingPiece.CanMoveTo(board, this, tile);

            return false;
        }

        private System.Drawing.Color GetColor()
        {
            return this.Color == BoardTileColor.White ? System.Drawing.Color.FromArgb(235, 236, 208) : System.Drawing.Color.FromArgb(137, 163, 105);
        }

        private void DrawIdentifier(Graphics graphics, float tileWidth, float tileHeight)
        {
            Font drawFont = new Font("Arial", (tileWidth < tileHeight) ? tileWidth / 6 : tileHeight / 6, FontStyle.Bold);

            float marginw = tileWidth / 30;
            float marginh = tileHeight / 30;

            // row number
            if (X == 0)
            {
                graphics.DrawString((ChessBoard.BoardSize - Y).ToString(), drawFont,
                    new SolidBrush(System.Drawing.Color.FromArgb(200, 0, 0, 0)),
                    X * tileWidth + marginw, Y * tileHeight + marginh);
            }

            // col letter
            if (Y + 1 == ChessBoard.BoardSize)
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Far;

                graphics.DrawString(((char)('a' + (char)X)).ToString(), drawFont,
                    new SolidBrush(System.Drawing.Color.FromArgb(200, 0, 0, 0)),
                    X * tileWidth + marginw, Y * tileHeight + tileHeight - marginh, format);
            }

            drawFont.Dispose();
        }

        internal void Draw(Graphics graphics, float tileWidth, float tileHeight)
        {
            if (State.HasFlag(TileState.Latest))
            {
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(247, 180, 86)),
                                new RectangleF(X * tileWidth, Y * tileHeight, tileWidth, tileHeight));
            }
            else if (State.HasFlag(TileState.Selected))
            {
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(247, 97, 86)),
                                new RectangleF(X * tileWidth, Y * tileHeight, tileWidth, tileHeight));

            }
            else if (State.HasFlag(TileState.Normal))
            {
                graphics.FillRectangle(new SolidBrush(GetColor()), new RectangleF(X * tileWidth, Y * tileHeight, tileWidth, tileHeight));
            }

            DrawIdentifier(graphics, tileWidth, tileHeight);

            OccupyingPiece?.Draw(graphics, X, Y, tileWidth, tileHeight);

            float marginw = tileWidth / 3;
            float marginh = tileHeight / 3;

            if (State.HasFlag(TileState.Targeted))
            {
                int padding = 3;
                graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(200, 50, 50, 50)), 
                    X * tileWidth + marginw,
                    Y * tileHeight + marginh,
                    tileWidth - marginw * 2, 
                    tileHeight - marginh * 2);

                graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(247, 97, 86)), 
                    X * tileWidth + marginw + padding, 
                    Y * tileHeight + marginh + padding,
                    tileWidth - (marginw + padding) * 2,
                    tileHeight - (marginh + padding) * 2);
            }
        }
    }
}
