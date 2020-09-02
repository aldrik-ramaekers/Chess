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
        public Pawn()
        {
            PieceImage = new Bitmap(Chess.Properties.Resources.b_pawn_png_shadow_256px);
        }

        public override void Draw(Graphics graphics, float x, float y, float tileWidth, float tileHeight)
        {
            if (PieceImage != null)
            {
                var image = this.ResizeImage(PieceImage, (int)tileWidth, (int)tileHeight);

                graphics.DrawImage(image, new PointF(x*tileWidth, y*tileHeight));

                image.Dispose();
            }
        }

        public override bool MoveTo()
        {
            throw new NotImplementedException();
        }
    }
}
