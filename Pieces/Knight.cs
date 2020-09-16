using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Knight : ChessPiece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_knight_png_shadow_256px : Chess.Properties.Resources.w_knight_png_shadow_256px);
        }

        public override bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            Point[] pointsToCheck = {
                new Point(currentTile.X - 2, currentTile.Y - 1),
                new Point(currentTile.X - 2, currentTile.Y + 1),

                new Point(currentTile.X + 2, currentTile.Y - 1),
                new Point(currentTile.X + 2, currentTile.Y + 1),

                new Point(currentTile.X + 1, currentTile.Y - 2),
                new Point(currentTile.X - 1, currentTile.Y - 2),

                new Point(currentTile.X + 1, currentTile.Y + 2),
                new Point(currentTile.X - 1, currentTile.Y + 2),
            };

            foreach(var point in pointsToCheck)
            {
                if (!(destinationTile.Y == point.Y && destinationTile.X == point.X)) continue;

                var enemy = board.PieceAt(point.X, point.Y);
                if (enemy == null) return true;
                if (enemy != null && enemy.IsWhite != this.IsWhite) return true;
                if (enemy != null && enemy != this) continue;
            }

            return false;
        }
    }
}
