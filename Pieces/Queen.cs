using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Queen : ChessPiece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            PieceImage = new Bitmap(!isWhite ? Chess.Properties.Resources.b_queen_png_shadow_256px : Chess.Properties.Resources.w_queen_png_shadow_256px);
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

            // diagonal 

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
