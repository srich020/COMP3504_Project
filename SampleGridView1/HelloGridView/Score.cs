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
using SQLite;


namespace HelloGridView
{
    
    public class Score
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } // auto set when isnerted to the db
        public int score { get; set; }
        public string name { get; set; }
       
        public Score() { } // must have a default constructor to use SQLite methods 

        public Score(int score,string name)
        {
            this.score = score;
            this.name = name;
        }

        public override string ToString() // called when object geven to list for default list display
        {
            string sc = score.ToString();
            return sc;
        }







    }
}