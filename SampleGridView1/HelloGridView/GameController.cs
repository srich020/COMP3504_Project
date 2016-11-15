using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HelloGridView
{
    class GameController
    {
        private static GameController cntr = null;
        private ColorSquare[,] board;

        public static GameController getInstance()
        {
            if (cntr == null)
                cntr = new GameController();

            return cntr;
        }

       

        private GameController()
        {
            board = new ColorSquare[6,6];
        }

        public int length() { return board.Length;  }

        public void loadBoard()
        {
            int[] colors = {Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                            Resource.Drawable.Red_static, Resource.Drawable.Yellow_static };

            int[] scolors = {Resource.Drawable.Blue_static, Resource.Drawable.Blue_static,
                            Resource.Drawable.Blue_static, Resource.Drawable.Blue_static };

            Random rng = new Random();
            for(int row=0; row < board.GetLength(0);row++)
            {
                for(int col=0; col < board.GetLength(1); col++)
                {
                   int n = rng.Next(colors.Length);
                    int c = colors[n];
                    int cs = scolors[n];
                    board[row, col] = new ColorSquare(c,cs,col,row);
                }

            }
        }

        public void updateWorldState(int position)
        {
            ColorSquare sel = get(position);
            sel.toggleSelected();


            //TODO game logic

            
        }

        public ColorSquare get(int x, int y)
        {
            if( x < 0 || x >= board.GetLength(1) ||
                y < 0 || y >= board.GetLength(0)
                )
                return null;
    
            return board[y,x];
        }


        

        public ColorSquare get(int position)
        {
            int y = (int)position / board.GetLength(0);
            int x = position % board.GetLength(1);
            return get(x, y);
        }


      


    }
}