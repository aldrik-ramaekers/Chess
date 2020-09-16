using Chess.Events;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public class ChessBoard
    {
        public const int BoardSize = 8;

        private float tileWidth;
        private float tileHeight;

        private MainForm container = null;
        private bool screenInvalidated = true;
        private List<BoardTile> latestTiles = new List<BoardTile>();
        private RoundFinishedEvent currentRountData = new RoundFinishedEvent(1);
        private int movesMadeWithoutProgress = 60;

        public BoardTile[,] Tiles;
        public BoardTile SelectedTile = null;

        /// <summary>
        /// Event that is evoked when the game has ended.
        /// </summary>
        public event EventHandler<GameFinishedEvent> OnGameFinished;

        /// <summary>
        /// Event that is evoked after drawing the board.
        /// </summary>
        public event EventHandler OnDraw;

        /// <summary>
        /// Event that is evoked after a round has been played.
        /// </summary>
        public event EventHandler<RoundFinishedEvent> OnRoundFinished;

        public ChessBoard(MainForm container)
        {
            this.container = container;

            GenerateBoard();
            GeneratePieces();

            DrawGame();
        }

        /// <summary>
        /// Handle the window resize event.
        /// </summary>
        public void HandleResize()
        {
            DrawGame();
        }

        /// <summary>
        /// Generate the pieces.
        /// </summary>
        public void GeneratePieces()
        {
#if false
            Tiles[0, 0].OccupyingPiece = new Rook(false);
            Tiles[0, 1].OccupyingPiece = new Knight(false);
            Tiles[0, 2].OccupyingPiece = new Bishop(false);
            Tiles[0, 3].OccupyingPiece = new Queen(false);
            Tiles[0, 4].OccupyingPiece = new King(false);
            Tiles[0, 5].OccupyingPiece = new Bishop(false);
            Tiles[0, 6].OccupyingPiece = new Knight(false);
            Tiles[0, 7].OccupyingPiece = new Rook(false);

            for (int x = 0; x < BoardSize; x++)
                Tiles[1, x].OccupyingPiece = new Pawn(false);

            for (int x = 0; x < BoardSize; x++)
                Tiles[6, x].OccupyingPiece = new Pawn(true);

            Tiles[7, 0].OccupyingPiece = new Rook(true);
            Tiles[7, 1].OccupyingPiece = new Knight(true);
            Tiles[7, 2].OccupyingPiece = new Bishop(true);
            Tiles[7, 3].OccupyingPiece = new Queen(true);
            Tiles[7, 4].OccupyingPiece = new King(true);
            Tiles[7, 5].OccupyingPiece = new Bishop(true);
            Tiles[7, 6].OccupyingPiece = new Knight(true);
            Tiles[7, 7].OccupyingPiece = new Rook(true);
#endif
#if false
            Tiles[0, 7].OccupyingPiece = new King(false);

            Tiles[1, 4].OccupyingPiece = new Queen(true);
            Tiles[2, 6].OccupyingPiece = new King(true);
#endif
#if true
            Tiles[0, 4].OccupyingPiece = new King(false);

            Tiles[1, 1].OccupyingPiece = new Pawn(true);
            Tiles[7, 4].OccupyingPiece = new King(true);
#endif
        }

        /// <summary>
        /// Get the piece at the given tile location.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public ChessPiece PieceAt(Point point)
        {
            return PieceAt(point.X, point.Y);
        }

        /// <summary>
        /// Get the piece at the given tile location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ChessPiece PieceAt(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < BoardSize && y < BoardSize)
            {
                return Tiles[y, x].OccupyingPiece;
            }

            return null;
        }

        /// <summary>
        /// Get tiles by the given type of the occupying type and color.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="isWhite"></param>
        /// <returns></returns>
        public List<BoardTile> TilesByPieceType(Type piece, bool isWhite)
        {
            List<BoardTile> result = new List<BoardTile>();

            for (int y = 0; y < ChessBoard.BoardSize; y++)
            {
                for (int x = 0; x < ChessBoard.BoardSize; x++)
                {
                    var found = PieceAt(x, y);

                    if (found != null && found.GetType() == piece && found.IsWhite == isWhite)
                    {
                        result.Add(Tiles[y, x]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a piece by the given type and color.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="isWhite"></param>
        /// <returns></returns>
        public ChessPiece PieceByType(Type piece, bool isWhite)
        {
            for (int y = 0; y < ChessBoard.BoardSize; y++)
            {
                for (int x = 0; x < ChessBoard.BoardSize; x++)
                {
                    var found = PieceAt(x, y);
                    if (found != null && found.GetType() == piece && found.IsWhite == isWhite)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Generate the chess board.
        /// </summary>
        public void GenerateBoard()
        {
            Tiles = new BoardTile[BoardSize, BoardSize];

            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    Tiles[y, x] = new BoardTile(x, y);
                }
            }
        }

        /// <summary>
        /// Draw the tile at the given point.
        /// </summary>
        /// <param name="point"></param>
        public void DrawTile(Point point)
        {
            if (point.X >= 0 && point.X < BoardSize && point.Y >= 0 && point.Y < BoardSize)
            {
                var tile = Tiles[point.Y, point.X];

                using (Graphics g = (screenInvalidated ? Graphics.FromImage(container.chessBoardBitmap.Image) : container.chessBoardBitmap.CreateGraphics()))
                {
                    tile.State = TileState.Normal;

                    if (latestTiles.Contains(tile))
                        tile.State |= TileState.Latest;

                    if (SelectedTile != null && SelectedTile.CanMoveTo(this, tile))
                        tile.State |= TileState.Targeted;

                    if (tile == SelectedTile)
                        tile.State |= TileState.Selected;

                    tile.Draw(g, tileWidth, tileHeight);
                }
            }
        }

        /// <summary>
        /// Invalidate the current frame and draw + display a new frame.
        /// </summary>
        public void DrawGame()
        {
            screenInvalidated = true;

            if (container.chessBoardBitmap.Image != null)
                container.chessBoardBitmap.Image.Dispose();

            container.chessBoardBitmap.Image = new Bitmap(container.chessBoardBitmap.Size.Width, container.chessBoardBitmap.Size.Height);

            tileWidth = container.chessBoardBitmap.Size.Width / (float)BoardSize;
            tileHeight = container.chessBoardBitmap.Size.Height / (float)BoardSize;

            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    DrawTile(new Point(x, y));
                }
            }

            OnDraw?.Invoke(this, null);

            screenInvalidated = false;
        }

        /// <summary>
        /// Convert mouse position relative to window to tile coordinates.
        /// </summary>
        /// <param name="x">Mouse X</param>
        /// <param name="y">Mouse Y</param>
        /// <returns></returns>
        public Point MouseToTilePosition(int x, int y)
        {
            return new Point((int)(x / tileWidth), (int)(y / tileHeight));
        }

        /// <summary>
        /// Checks whether the given piece can move to any spot.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private bool CanMoveToAnything(BoardTile tile)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if (tile.OccupyingPiece != null && tile.CanMoveTo(this, Tiles[y, x]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsCheckmate(Player player)
        {
            bool kingUnderAttack = false;
            bool canMove = false;
            var king = TilesByPieceType(typeof(King), !player.IsWhite).FirstOrDefault();

            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if (Tiles[y, x].OccupyingPiece?.IsWhite == player.IsWhite)
                    {
                        if (Tiles[y, x].OccupyingPiece != null && Tiles[y, x].CanMoveTo(this, king))
                        {
                            kingUnderAttack = true;
                        }
                    }
                    else if (CanMoveToAnything(Tiles[y, x])) canMove = true;
                }
            }

            return kingUnderAttack && !canMove;
        }

        private bool IsStalemate(bool moveMadeByWhite)
        {
            if (IsKingAlone(!moveMadeByWhite))
            {
                if (!CanMoveToAnything(TilesByPieceType(typeof(King), !moveMadeByWhite).FirstOrDefault())) return true;
            }

            return false;
        }

        private bool IsKingAlone(bool isWhite)
        {
            if (PieceByType(typeof(Pawn), isWhite) != null) return false;
            if (PieceByType(typeof(Rook), isWhite) != null) return false;
            if (PieceByType(typeof(Knight), isWhite) != null) return false;
            if (PieceByType(typeof(Bishop), isWhite) != null) return false;
            if (PieceByType(typeof(Queen), isWhite) != null) return false;

            return true;
        }

        private bool EnoughMaterialOnBoard()
        {
            if (IsKingAlone(true) && IsKingAlone(false)) return false;

            return true;
        }

        /// <summary>
        /// Try to move the currently selected piece to the given board tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns>Returns true if movement was successfull.</returns>
        private bool MoveSelectedPieceTo(BoardTile tile, Player player)
        {
            if (SelectedTile.CanMoveTo(this, tile))
            {
                // Check if current move is a castling move
                if (SelectedTile.OccupyingPiece != null && SelectedTile.OccupyingPiece.GetType() == typeof(King) && 
                    tile.OccupyingPiece != null && tile.OccupyingPiece.GetType() == typeof(Rook) && 
                    tile.OccupyingPiece.IsWhite == SelectedTile.OccupyingPiece.IsWhite)
                {
                    var rookDestination = ((Rook)tile.OccupyingPiece).GetCastleLocation(this, tile);
                    rookDestination.OccupyingPiece = tile.OccupyingPiece;
                    tile.OccupyingPiece = null;

                    tile = ((King)SelectedTile.OccupyingPiece).GetCastleLocation(this, SelectedTile, tile);
                }

                currentRountData.AddMove(new PlayerMove(player, SelectedTile, tile));

                latestTiles.Clear();
                latestTiles.Add(tile);
                latestTiles.Add(SelectedTile);

                if (tile.OccupyingPiece == null && SelectedTile.OccupyingPiece.GetType() != typeof(Pawn))
                {
                    movesMadeWithoutProgress++;
                }
                else
                {
                    movesMadeWithoutProgress = 0;
                }

                tile.OccupyingPiece = SelectedTile.OccupyingPiece;
                SelectedTile.OccupyingPiece.PostMovementEvent(tile);
                SelectedTile.OccupyingPiece = null;

                if (IsCheckmate(player))
                {
                    OnGameFinished?.Invoke(this, new GameFinishedEvent(player, GameFinishedReason.CheckMate));
                }
                else if (!EnoughMaterialOnBoard())
                {
                    OnGameFinished?.Invoke(this, new GameFinishedEvent(null, GameFinishedReason.InsufficientMaterial));
                }
                else if (IsStalemate(player.IsWhite))
                {
                    OnGameFinished?.Invoke(this, new GameFinishedEvent(null, GameFinishedReason.Stalemate));
                }
                else if (movesMadeWithoutProgress >= 75)
                {
                    OnGameFinished?.Invoke(this, new GameFinishedEvent(null, GameFinishedReason.Move75));
                }

                if (currentRountData.IsDone)
                {
                    OnRoundFinished?.Invoke(this, currentRountData);
                    currentRountData = new RoundFinishedEvent(currentRountData.Round + 1);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Select a tile.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Returns true if the turn should be ended for current player.</returns>
        public bool SelectTile(Point point, Player player)
        {
            bool result = false;

            var clickedTile = Tiles[point.Y, point.X];

            if (SelectedTile != clickedTile)
            {
                if (SelectedTile != null)
                {
                    if (MoveSelectedPieceTo(clickedTile, player))
                    {
                        SelectedTile = null;
                        result = true;
                    }
                    else
                    {
                        // Only allow selection if player's own pieces.
                        if (player.IsWhite == clickedTile.OccupyingPiece?.IsWhite)
                            SelectedTile = clickedTile;
                        else
                            SelectedTile = null;
                    }
                }
                else
                {
                    // Only allow selection if player's own pieces.
                    if (player.IsWhite == clickedTile.OccupyingPiece?.IsWhite)
                        SelectedTile = clickedTile;
                    else
                        SelectedTile = null;
                }
            }
            else
            {
                SelectedTile = null;
            }

            DrawGame();

            return result;
        }
    }
}
