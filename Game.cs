using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Events;
using Chess.Extensions;

namespace Chess
{
    public enum GameState
    {
        Playing,
        Ended,
    }

    public class Game
    {
        List<Player> Players = new List<Player>();

        private ChessBoard board = null;
        private MainForm container;
        private GameState state;
        private GameFinishedEvent gameFinishData;

        public Game(MainForm container)
        {
            this.state = GameState.Playing;
            this.container = container;

            Players.Add(new Player(true, true));
            Players.Add(new Player(false, false));

            board = new ChessBoard(container);

            board.OnGameFinished += Board_OnGameFinished;
            board.OnDraw += Board_OnDraw;
            board.OnRoundFinished += Board_OnRoundFinished;
        }

        private void Board_OnRoundFinished(object sender, RoundFinishedEvent e)
        {
            this.container.moveList.Items.Add(new ListViewItem(new string[] { e.Round.ToString(), e.Move1.ToString(), e.Move2.ToString() }));
        }

        private void Board_OnDraw(object sender, EventArgs e)
        {
            if (state == GameState.Ended)
            {
                using (Graphics g = Graphics.FromImage(container.chessBoardBitmap.Image))
                {
                    Font drawFont = new Font("Arial", 28, FontStyle.Bold);

                    string endingName = "";
                    if (gameFinishData.Winner == null) endingName = "Game tied\n";
                    else endingName = gameFinishData.GetWinnerName() + " won\n";
                    endingName += "by " + gameFinishData.GetFinishReason();

                    int w = 350;
                    int ph = g.ParagraphHeight(endingName, drawFont);
                    int h = ph + 80;

                    g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(80, 80, 80)), (this.container.chessBoardBitmap.Width / 2) - (w / 2), (this.container.chessBoardBitmap.Height / 2) - (h / 2), w, h, 20);

                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;

                    Color textColor = Color.FromArgb(245, 245, 245);

                    g.DrawParagraph(endingName, drawFont,
                         new SolidBrush(textColor), (this.container.chessBoardBitmap.Width / 2), (this.container.chessBoardBitmap.Height / 2) - (ph / 2), format);
                }
            }
        }

        private void Board_OnGameFinished(object sender, Events.GameFinishedEvent e)
        {
            state = GameState.Ended;
            gameFinishData = e;
        }

        public void HandleResize()
        {
            board.HandleResize();
        }

        private Player NextPlayer(Player p)
        {
            bool isNext = false;
            foreach (var player in Players)
            {
                if (isNext)
                {
                    return player;
                }

                if (p == player) isNext = true;
            }

            if (Players.Count != 0)
                return Players.ElementAt(0);
            else
                return null;
        }

        public void HandleClick(int x, int y)
        {
            if (state != GameState.Playing) return;

            Point clickedTileCoords = board.MouseToTilePosition(x, y);

            foreach (var player in Players)
            {
                if (player.IsPlayersTurn)
                {
                    bool endTurn = board.SelectTile(clickedTileCoords, player);

                    if (endTurn)
                    {
                        player.IsPlayersTurn = false;
                        Player nextPlayer = NextPlayer(player);

                        if (nextPlayer != null)
                            nextPlayer.IsPlayersTurn = true;
                    }
                }
            }
        }
    }
}
