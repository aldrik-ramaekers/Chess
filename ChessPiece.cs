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

        internal virtual MoveCheckResult CanMoveUpLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveUpRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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


        internal virtual MoveCheckResult CanMoveDownRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveDownLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveRight(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveDown(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal virtual MoveCheckResult CanMoveUp(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
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

        internal bool WillKingBeAttackedIfMovedTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            bool result = false;

            var friendlyKingTile = board.TilesByPieceType(typeof(Chess.Pieces.King), this.IsWhite).FirstOrDefault();

            // Temporarely move piece to destination to check if king will be attacked.
            ChessPiece tempHolder = destinationTile.OccupyingPiece;
            destinationTile.OccupyingPiece = currentTile.OccupyingPiece;

            if (currentTile != destinationTile)
                currentTile.OccupyingPiece = null;

            if (this.GetType() == typeof(Chess.Pieces.King))
            {
                friendlyKingTile = destinationTile;
            }

            for (int y = 0; y < ChessBoard.BoardSize; y++)
            {
                for (int x = 0; x < ChessBoard.BoardSize; x++)
                {
                    var tileToCheck = board.Tiles[y, x];

                    if (tileToCheck.OccupyingPiece != null && tileToCheck.OccupyingPiece != this && tileToCheck.OccupyingPiece.CanMoveTo(board, tileToCheck, friendlyKingTile))
                    {
                        result = true;
                        break;
                    }
                }
            }

            // Move pieces back.
            currentTile.OccupyingPiece = destinationTile.OccupyingPiece;
            destinationTile.OccupyingPiece = tempHolder;

            return result;
        }

        public virtual void PostMovementEvent(BoardTile tile)
        {
            
        }

        public abstract bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile);

        public void Draw(Graphics graphics, float x, float y, float tileWidth, float tileHeight)
        {
            if (PieceImage != null)
            {
                int imgW = (int)(tileWidth / 1.3);
                int imgH = (int)(tileHeight / 1.3);

                var image = ImageHelper.ResizeImage(PieceImage, imgW, imgH);

                graphics.DrawImage(image, new PointF(x * tileWidth + (tileWidth / 2) - (imgW / 2), y * tileHeight + (tileHeight / 2) - (imgH / 2)));

                image.Dispose();
            }
        }
    }
}
