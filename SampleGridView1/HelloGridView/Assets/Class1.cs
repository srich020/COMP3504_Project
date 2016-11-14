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

namespace App7.Assets
{
    class colorSquare
    {
        public int colorValue;
        public int xLoc;
        public int yLoc;
        public ImageView displayImage;

        public colorSquare(int cVal, int xLo, int yLo)
        {
            colorValue = cVal;
            xLoc = xLo;
            yLoc = yLo;
            //based on cVal, set the appropriate color image/icon resource
        }

        public void selected()
        {
            //when selected, change image to selected image
        }

        public void deselected()
        {
            //called if a pattern didn't match, and squares are no eliminated
        }

        public void deleteSelf()
        {
            //must delete itself when called
        }

        public void dropDown()
        {
            //additional functionality
        }

        public int getColorValue()
        {
            return colorValue;
        }
        public int getXLoc()
        {
            return xLoc;
        }
        public int getYLoc()
        {
            return yLoc;
        }
        public void setColorValue(int c)
        {
            colorValue = c;
        }
        public void setYLoc(int y)
        {
            yLoc = y;
        }
        public void setXLoc(int x)
        {
            xLoc = x;
        }
    }
}