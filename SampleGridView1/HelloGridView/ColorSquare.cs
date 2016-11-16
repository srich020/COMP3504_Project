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

        public ColorSquare(int cVal, int csVal, int xLo, int yLo, int cNum)
        {
            colorValue = cVal;
            colorSelValue = csVal;
            xLoc = xLo;
            yLoc = yLo;
            selected = false;
            colorNum = cNum;
        }

        public int showColor()
        {
            return selected ? colorSelValue : colorValue;
        }
       
        public void toggleSelected()
        {
            selected = !selected;
            
        }


       
    }
}