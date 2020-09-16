using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Events
{
    public enum GameFinishedReason
    { 
        CheckMate,
        InsufficientMaterial,
        Stalemate,

        /// <summary>
        /// Not implemented from here on.
        /// </summary>
        Resignation,
        Move75,
        Repetition,
        Agreement,
    }

    public class GameFinishedEvent : EventArgs
    {
        public Player Winner { get; }
        public GameFinishedReason Reason { get; set; }

        public GameFinishedEvent(Player winner, GameFinishedReason reason)
        {
            Winner = winner;
            Reason = reason;
        }

        public string GetWinnerName()
        {
            if (Winner != null)
            {
                return Winner.IsWhite ? "White" : "Black";
            }

            return "";
        }

        public string GetFinishReason()
        {
            return Reason.ToString();
        }
    }
}
