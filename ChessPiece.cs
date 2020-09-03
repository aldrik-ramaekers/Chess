using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Chess
{
    internal enum MoveCheckResult
    {
        CanMove,
        CantMove,
        ContinueCheck,
    }

    public abstract class ChessPiece
    {
        internal Bitmap PieceImage;
        internal bool IsWhite;

        public ChessPiece(bool isWhite)
        {
            IsWhite = isWhite;
        }

        internal MoveCheckResult CanMoveUpLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            for (int t = 1; t <= count; t++)
            {
                Point p1 = new Point(currentTile.X - t, currentTile.Y - t);

                var enemy = board.PieceAt(p1.X, p1.Y);
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy == null)) return MoveCheckResult.CanMove;
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveUpRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            for (int t = 1; t <= count; t++)
            {
                Point p1 = new Point(currentTile.X + t, currentTile.Y - t);

                var enemy = board.PieceAt(p1.X, p1.Y);
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy == null)) return MoveCheckResult.CanMove;
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
            }

            return MoveCheckResult.ContinueCheck;
        }


        internal MoveCheckResult CanMoveDownRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            for (int t = 1; t <= count; t++)
            {
                Point p1 = new Point(currentTile.X + t, currentTile.Y + t);

                var enemy = board.PieceAt(p1.X, p1.Y);
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy == null)) return MoveCheckResult.CanMove;
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveDownLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            for (int t = 1; t <= count; t++)
            {
                Point p1 = new Point(currentTile.X - t, currentTile.Y + t);

                var enemy = board.PieceAt(p1.X, p1.Y);
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy == null)) return MoveCheckResult.CanMove;
                if ((p1.X == destinationTile.X && p1.Y == destinationTile.Y) && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            if (currentTile.Y != destinationTile.Y) return MoveCheckResult.ContinueCheck;

            if (destinationTile.X < currentTile.X)
            {
                for (int t = 1; t <= count; t++)
                {
                    var enemy = board.PieceAt(currentTile.X - t, currentTile.Y);
                    if (currentTile.X - t == destinationTile.X && (enemy == null)) return MoveCheckResult.CanMove;
                    if (currentTile.X - t == destinationTile.X && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                    if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
                }
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            if (currentTile.Y != destinationTile.Y) return MoveCheckResult.ContinueCheck;

            if (destinationTile.X > currentTile.X)
            {
                for (int t = 1; t <= count; t++)
                {
                    var enemy = board.PieceAt(currentTile.X + t, currentTile.Y);
                    if (currentTile.X + t == destinationTile.X && (enemy == null)) return MoveCheckResult.CanMove;
                    if (currentTile.X + t == destinationTile.X && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                    if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
                }
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveDown(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            if (currentTile.X != destinationTile.X) return MoveCheckResult.ContinueCheck;

            if (destinationTile.Y > currentTile.Y)
            {
                for (int t = 1; t <= count; t++)
                {
                    var enemy = board.PieceAt(currentTile.X, currentTile.Y + t);
                    if (currentTile.Y + t == destinationTile.Y && (enemy == null)) return MoveCheckResult.CanMove;
                    if (currentTile.Y + t == destinationTile.Y && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                    if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
                }
            }

            return MoveCheckResult.ContinueCheck;
        }

        internal MoveCheckResult CanMoveUp(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            if (currentTile.X != destinationTile.X) return MoveCheckResult.ContinueCheck;

            if (destinationTile.Y < currentTile.Y)
            {
                for (int t = 1; t <= count; t++)
                {
                    var enemy = board.PieceAt(currentTile.X, currentTile.Y - t);
                    if (currentTile.Y - t == destinationTile.Y && (enemy == null)) return MoveCheckResult.CanMove;
                    if (currentTile.Y - t == destinationTile.Y && (enemy != null && enemy.IsWhite != this.IsWhite)) return MoveCheckResult.CanMove;
                    if (enemy != null && enemy != this) return MoveCheckResult.ContinueCheck;
                }
            }

            return MoveCheckResult.ContinueCheck;
        }

        public abstract bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile);
        public void Draw(Graphics graphics, float x, float y, float tileWidth, float tileHeight)
        {
            if (PieceImage != null)
            {
                var image = ImageHelper.ResizeImage(PieceImage, (int)tileWidth, (int)tileHeight);

                graphics.DrawImage(image, new PointF(x * tileWidth + (tileWidth/ 40), y * tileHeight + (tileWidth / 40)));

                image.Dispose();
            }
        }
    }
}
