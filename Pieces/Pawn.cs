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
            }
            else
            {
                result = CanMoveUp(board, currentTile, destinationTile, space);
                if (result == MoveCheckResult.CanMove) return true;
                if (result == MoveCheckResult.CantMove) return false;
            }

            return false;
        }  
    }
}
