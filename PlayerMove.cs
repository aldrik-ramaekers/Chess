using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class PlayerMove
    {
        public Player Player { get; }
        public BoardTile From { get; }
        public BoardTile To { get; }

        public PlayerMove(Player player, BoardTile from, BoardTile to)
        {
            Player = player;
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return From.ToString() + " -> " + To.ToString();
        }
    }
}
