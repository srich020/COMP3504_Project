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
        private int[] colors={Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                            Resource.Drawable.Red_static, Resource.Drawable.Yellow_static };
        //private Activity1 gameRef = new Activity1();

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
          

            int[] scolors = {Resource.Drawable.Blue_selected, Resource.Drawable.Green_selected,
                            Resource.Drawable.Red_selected, Resource.Drawable.Yellow_selected };

            Random rng = new Random();
            for(int row=0; row < board.GetLength(0);row++)
            {
                for(int col=0; col < board.GetLength(1); col++)
                {
                   int n = rng.Next(colors.Length);
                    int c = colors[n];
                    int cs = scolors[n];
                    board[row, col] = new ColorSquare(c,cs,col,row,n);
                }
                //col = x; row= = y
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

        public String[] getRandPattern()
        {
            Random rng = new Random();
            int patternSize= 3;
        
            String[] stringPatt = new String[3]; 
            for (int i=0;i<patternSize;i++){
                int num = rng.Next(colors.Length);
                switch (num)
                {
                    case 0: stringPatt[i] ="Blue"; break;
                    case 1: stringPatt[i] = "Green"; break;
                    case 2: stringPatt[i] = "Red"; break;
                    case 3: stringPatt[i] = "Yellow"; break;
                }
            }
            return stringPatt;
        }
      
        public void deToggleAll()
        {
            for (int row2 = 0; row2 < board.GetLength(0); row2++)
            {
                for (int col2 = 0; col2 < board.GetLength(1); col2++)
                {
                    board[row2, col2].selected = false;
                }

            }
        }

        //takes in 2 colorsquares, returns true if they are in the same X column
        public bool sameX(ColorSquare square1, ColorSquare square2)
        {
            bool result = false;
            if (square1.xLoc==square2.xLoc)
            {
                result = true;
            }

            return result;
        }
        //col = x; row= = y
        //takes in 2 colorsquares, returns true if they are in the same Y row
        public bool sameY(ColorSquare square1, ColorSquare square2)
        {
            bool result = false;
            if (square1.yLoc == square2.yLoc)
            {
                result = true;
            }

            return result;
        }


        public bool processMatch(ColorSquare[] selectedSquares)
        {
            //can tell if pattern is properly matched 
            bool matchResult = false;
            if ( ( sameX(selectedSquares[0], selectedSquares[1]) && sameX(selectedSquares[1], selectedSquares[2]) )
                || (sameY(selectedSquares[0], selectedSquares[1]) && sameY(selectedSquares[1], selectedSquares[2]) )
                || (sameX(selectedSquares[0], selectedSquares[1]) && sameY(selectedSquares[1], selectedSquares[2]) )
                || (sameY(selectedSquares[0], selectedSquares[1]) && sameX(selectedSquares[1], selectedSquares[2]))
                )
            {
                for (int s = 0; s < selectedSquares.Length; s++)
                {
                    selectedSquares[s].toggleSelected();
                    selectedSquares[s].randomizeColor();
                }
                randomizeBetween(selectedSquares[0], selectedSquares[1]);
                randomizeBetween(selectedSquares[1], selectedSquares[2]);
                matchResult = true;
            }


            return matchResult;

        }
        
        //takes 2 ColorSquare objects. will randomize all elements in between
        private void randomizeBetween(ColorSquare square1, ColorSquare square2)
        {
            if (sameX(square1, square2)) //same Column
            {
                if (square1.yLoc > square2.yLoc) //1 further down the grid than 2
                {
                    int diff = square1.yLoc - square2.yLoc; //-1 to avoid outofbounds
                    int start = square1.yLoc;
                    for (int i=0;i<diff; i++)
                    {
                        cntr.get(square1.xLoc, start--).randomizeColor();
                    }
                }
                if (square1.yLoc < square2.yLoc) //1 higher up the grid than 2
                {
                    int diff = square2.yLoc - square1.yLoc; //-1 to avoid outofbounds
                    int start = square2.yLoc;
                    for (int i = 0; i < diff; i++)
                    {
                        cntr.get(square2.xLoc, start--).randomizeColor();
                    }
                }
            }

            if (sameY(square1, square2)) //same row
            {
                if (square1.xLoc > square2.xLoc) //1 further down the grid than 2
                {
                    int diff = square1.xLoc - square2.xLoc; //-1 to avoid outofbounds
                    int start = square1.xLoc;
                    for (int i = 0; i < diff; i++)
                    {
                        cntr.get(start--, square1.yLoc).randomizeColor();
                    }
                }
                if (square1.xLoc < square2.xLoc) //1 higher up the grid than 2
                {
                    int diff = square2.xLoc - square1.xLoc; //-1 to avoid outofbounds
                    int start = square2.xLoc;
                    for (int i = 0; i < diff; i++)
                    {
                        cntr.get(start--, square2.yLoc).randomizeColor();
                    }
                }
            }
        }

    }
}