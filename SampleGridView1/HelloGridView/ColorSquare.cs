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
    class ColorSquare
    {
        public int colorValue { get; set; }
        public int colorSelValue { get; set; }
        public int colorNum { get; set; }
        public int xLoc { get; set; }
        public int yLoc { get; set; }
        public bool selected { get; set;  }
        private int[] colors ={Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                            Resource.Drawable.Red_static, Resource.Drawable.Yellow_static };
        int[] scolors = {Resource.Drawable.Blue_selected, Resource.Drawable.Green_selected,
                            Resource.Drawable.Red_selected, Resource.Drawable.Yellow_selected };
        Random rng;

        public ColorSquare(int cVal, int csVal, int xLo, int yLo, int cNum)
        {
            colorValue = cVal;
            colorSelValue = csVal;
            colorNum = cNum;
            xLoc = xLo;
            yLoc = yLo;
            selected = false;
            rng = new Random();
        }

        public int showColor()
        {
            return selected ? colorSelValue : colorValue;
        }
       
        public void toggleSelected()
        {
            selected = !selected;
            
        }

        public void randomizeColor()
        {
            
            int newVal = rng.Next(4);

            while (newVal == colorNum)
            {
                newVal = rng.Next(colors.Length);
            }
            
            colorValue = colors[newVal];
            colorSelValue = scolors[newVal];
            colorNum = newVal;
        }
       
    }
}