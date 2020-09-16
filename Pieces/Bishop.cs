using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Bishop : ChessPiece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_bishop_png_shadow_256px : Chess.Properties.Resources.w_bishop_png_shadow_256px);
        }

        public override bool CanMoveTo(ChessBoard board, BoardTile currentTile, BoardTile destinationTile)
        {
            MoveCheckResult result;

            result = CanMoveDownLeft(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveDownRight(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUpLeft(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            result = CanMoveUpRight(board, currentTile, destinationTile);
            if (result == MoveCheckResult.CanMove) return true;
            if (result == MoveCheckResult.CantMove) return false;

            return false;
        }

       
    }
}
