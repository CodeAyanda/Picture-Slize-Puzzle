using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Picture_Slide_Puzzle
{
    public class Piece
    {
        public Image image;
        public Rectangle rect;
        public int number;
        public bool blank;

        public Piece(Image img, Rectangle rect, int number)
        {
            this.image = img;
            this.rect = rect;
            this.number = number;
            this.blank = false;
        }

        public void Show(Graphics g)
        {
            if (blank)
            {
                g.FillRectangle(new SolidBrush(Color.Black), rect);
            }
            else
            {
                g.DrawImageUnscaled(image, rect.X, rect.Y);

            }
        }
    }
}
