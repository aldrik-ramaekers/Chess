using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Pawn : ChessPiece
    {
        private bool firstMove = true;

        public Pawn(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_pawn_png_shadow_256px : Chess.Properties.Resources.w_pawn_png_shadow_256px);
        }

        public override void PostMovementEvent(BoardTile tile)
        {
            firstMove = false;

            // Pawn upgrade to queen
            if (tile.OccupyingPiece == this)
            {
                if ((this.IsWhite && tile.Y == 0) || (!this.IsWhite && tile.Y == 7))
                {
                    tile.OccupyingPiece = new Queen(this.IsWhite);
                }
            }

            base.PostMovementEvent(tile);
        }

        internal override MoveCheckResult CanMoveDown(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
        {
            var result = base.CanMoveDown(board, currentTile, destinationTile, count);

            if (result == MoveCheckResult.CanMove)
            {
                if (destinationTile.OccupyingPiece != null) result = MoveCheckResult.CantMove;
            }

            return result;
        }

        internal override MoveCheckResult CanMoveUp(ChessBoard board, BoardTile currentTile, BoardTile destinationTile, int count = ChessBoard.BoardSize)
        {
            var result = base.CanMoveUp(board, currentTile, destinationTile, count);

            if (result == MoveCheckResult.CanMove)
            {
                if (destinationTile.OccupyingPiece != null) result = MoveCheckResult.CantMove;
            }

            return result;
        }

        public override bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            MoveCheckResult result;

            int space = 2;
            if (!firstMove) space = 1;

            if (!IsWhite)
            {
                result = CanMoveDown(board, currentTile, destinationTile, space);
                if (result == MoveCheckResult.CanMove) return true;
                if (result == MoveCheckResult.CantMove) return false;

                // take left
                result = CanMoveDownLeft(board, currentTile, destinationTile, 1);
                if (result == MoveCheckResult.CanMove && destinationTile.OccupyingPiece != null && destinationTile.OccupyingPiece.IsWhite != IsWhite) return true;
                if (result == MoveCheckResult.CantMove) return false;

                // take right
                result = CanMoveDownRight(board, currentTile, destinationTile, 1);
                if (result == MoveCheckResult.CanMove && destinationTile.OccupyingPiece != null && destinationTile.OccupyingPiece.IsWhite != IsWhite) return true;
                if (result == MoveCheckResult.CantMove) return false;
            }
            else
            {
                result = CanMoveUp(board, currentTile, destinationTile, space);
                if (result == MoveCheckResult.CanMove) return true;
                if (result == MoveCheckResult.CantMove) return false;

                // take left
                result = CanMoveUpLeft(board, currentTile, destinationTile, 1);
                if (result == MoveCheckResult.CanMove && destinationTile.OccupyingPiece != null && destinationTile.OccupyingPiece.IsWhite != IsWhite) return true;
                if (result == MoveCheckResult.CantMove) return false;

                // take right
                result = CanMoveUpRight(board, currentTile, destinationTile, 1);
                if (result == MoveCheckResult.CanMove && destinationTile.OccupyingPiece != null && destinationTile.OccupyingPiece.IsWhite != IsWhite) return true;
                if (result == MoveCheckResult.CantMove) return false;
            }

            return false;
        }  
    }
}
