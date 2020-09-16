using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Events
{
    public class RoundFinishedEvent : EventArgs
    {
        public int Round { get; }
        public PlayerMove Move1 { get; private set; }
        public PlayerMove Move2 { get; private set; }

        public bool IsDone { get { return Move1 != null && Move2 != null; } }

        public RoundFinishedEvent(int round)
        {
            Round = round;
        }

        public void AddMove(PlayerMove move)
        {
            if (Move1 == null) Move1 = move;
            else Move2 = move;
        }
    }
}
