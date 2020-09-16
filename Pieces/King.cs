using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class King : ChessPiece
    {
        bool isInInitialPosition = true;

        public King(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_king_png_shadow_256px : Chess.Properties.Resources.w_king_png_shadow_256px);
        }

        public override void PostMovementEvent(BoardTile tile)
        {
            isInInitialPosition = false;

            base.PostMovementEvent(tile);
        }

        internal override MoveCheckResult CanMoveLeft(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = 8)
        {
            MoveCheckResult result = base.CanMoveLeft(board, currentTile, destinationTile, count);

            // Check for legal castling move
            if (isInInitialPosition)
            {
                var rooks = board.TilesByPieceType(typeof(Rook), this.IsWhite);

                foreach(var rook in rooks)
                {
                    if (rook != destinationTile) continue;

                    if (rook.OccupyingPiece is Rook piece)
                    {
                        if (piece.IsInInitialPosition)
                        {
                            if (rook.X == 0)
                            {
                                // check right path
                                if (board.Tiles[currentTile.Y, currentTile.X - 1].OccupyingPiece == null && rook.CanMoveTo(board, board.Tiles[currentTile.Y, currentTile.X - 1]))
                                {
                                    result = MoveCheckResult.CanMove;
                                }
                            }
                            else if (rook.X == 7)
                            {
                                // check left path
                                if (board.Tiles[currentTile.Y, currentTile.X + 1].OccupyingPiece == null && rook.CanMoveTo(board, board.Tiles[currentTile.Y, currentTile.X + 1]))
                                {
                                    result = MoveCheckResult.CanMove;
                                }
                            }
                        }
                    }
                }

            }

            return result;
        }

        public override bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            MoveCheckResult result;

            result = CanMoveDown(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUp(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveLeft(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveRight(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            // diagonal 

            result = CanMoveDownLeft(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveDownRight(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUpLeft(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUpRight(board, currentTile, destinationTile, 1);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            return false;
        }

        internal BoardTile GetCastleLocation(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            if (destinationTile.X < currentTile.X)
            {
                return board.Tiles[currentTile.Y, currentTile.X - 2];
            }
            else if (destinationTile.X > currentTile.X)
            {
                return board.Tiles[currentTile.Y, currentTile.X + 2];
            }

            return destinationTile;
        }
    }
}
