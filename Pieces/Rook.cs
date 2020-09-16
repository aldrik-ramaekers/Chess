using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Rook : ChessPiece
    {
        public bool IsInInitialPosition { get; private set; }

        public Rook(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_rook_png_shadow_256px : Chess.Properties.Resources.w_rook_png_shadow_256px);
            IsInInitialPosition = true;
        }

        public override void PostMovementEvent(BoardTile tile)
        {
            this.IsInInitialPosition = false;

            base.PostMovementEvent(tile);
        }

        public override bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            MoveCheckResult result;

            result = CanMoveDown(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUp(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveLeft(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveRight(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            return false;
        }

        internal BoardTile GetCastleLocation(ChessBoard board, BoardTile tile)
        {
            if (tile.X == 0)
            {
                return board.Tiles[tile.Y, tile.X + 3];
            }
            else if (tile.X == 7)
            {
                return board.Tiles[tile.Y, tile.X - 2];
            }

            return tile;
        }
    }
}
