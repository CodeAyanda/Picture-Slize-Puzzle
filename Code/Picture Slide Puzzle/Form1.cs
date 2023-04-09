using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Slide_Puzzle
{
    public partial class Form1 : Form
    {
        Bitmap dog;


        static int imgSize = 800;
        static int num = 4;
        static int cols = num; 
        static int rows = num;
        int PieceSize = imgSize/num;
        Piece[] pieces;
        Piece[] board;

        public Form1()
        {
            InitializeComponent();
            pieces = new Piece[cols * rows];
            board = new Piece[cols * rows];

            dog = new Bitmap("dog.jpg");
            int number = 0;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Rectangle rect = new Rectangle(j*PieceSize, i * PieceSize, PieceSize-2, PieceSize-2);
                    Image img = dog.Clone(rect, dog.PixelFormat);
                    Piece newPiece = new Piece(img, rect, number);
                    if (number == (cols * rows) - 1)
                    {
                        newPiece.blank = true;
                    }
                    pieces[number] = newPiece;
                    board[number] = newPiece;
                    number++;
                    
                }

            }
            Shuffler(100);
        }

        public void Movee(Piece[] arr, int x, int y)
        {
            Rectangle tempRect = arr[x].rect;
            int tempNumber = arr[x].number;


            arr[x].rect = arr[y].rect;
            arr[x].number = arr[y].number;

            arr[y].rect = tempRect;
            arr[y].number = tempNumber;


        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < (cols * rows); i++)
            {
                var piece = board[i];
                piece.Show(g);
            }
        }

        public bool IsNeighbour(int i, int j)
        {
            if(Math.Abs(board[i].rect.X - board[j].rect.X) == (imgSize/num) && board[i].rect.Y == board[j].rect.Y)
            {
                return true;
            }else if(Math.Abs(board[i].rect.Y - board[j].rect.Y) == (imgSize / num) && board[i].rect.X == board[j].rect.X)
            {
                return true;
            }
            return false;
        }

        public void Shuffler(int howMany)
        {
            for (int j = 0; j < howMany; j++)
            {
                List<Piece> neighbours = new List<Piece>();
                for (int i = 0; i < (cols * rows); i++)
                {
                    Piece current = board[i];
                    if (IsNeighbour(i, (cols * rows - 1)))
                    {
                        neighbours.Add(current);
                    }
                }
                Random rnd = new Random();
                int index = rnd.Next(neighbours.Count);
                Movee(board, neighbours[index].number, (cols * rows) - 1);
                neighbours.Clear();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point mouse = this.PointToClient(Cursor.Position);
            for (int i = 0; i < (cols*rows); i++)
            {
                if (board[i].rect.Contains(mouse))
                {
                    if(IsNeighbour(i, (cols * rows) - 1))
                    {
                        Movee(board, i, (cols * rows) - 1);

                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
