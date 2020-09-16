using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Player
    {
        public bool IsWhite;
        public bool IsPlayersTurn;

        public Player(bool isWhite, bool isPlayersTurn)
        {
            IsWhite = isWhite;
            IsPlayersTurn = isPlayersTurn;
        }
    }
}
