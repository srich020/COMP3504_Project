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
    public class Student
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } // auto set when isnerted to the db
        public string name { get; set; }

        public Student() { } // must have a default constructor to use SQLite methods 

        public Student(string name)
        {
            this.name = name;
        }

       
   
        public override string ToString() // called when object geven to list for default list display
        {
            return name; 
        }

    }
}