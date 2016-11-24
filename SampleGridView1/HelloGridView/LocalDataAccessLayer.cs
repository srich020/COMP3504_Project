using System;
using System.Collections.Generic;
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
    class LocalDataAccessLayer
    {
        //Code for singlton design pattern setup
        private static LocalDataAccessLayer data = null;
        public static LocalDataAccessLayer getInstance()
        {
            if(data == null)
                data = new LocalDataAccessLayer();

            return data;
        }

        //Regular class data and methods
        private SQLiteConnection dbConnection = null;

        /*=====================================================================
        * Constructor
        =====================================================================*/
        private LocalDataAccessLayer()
        {
            setUpDB();
        }

        /*=====================================================================
         * Deconstructor (Called when the object is destroyed)
         * closes connection to the database
          =====================================================================*/
        ~LocalDataAccessLayer()
        {
            shutDown();
        }

        /*=====================================================================
        * Deconstructor (Called when the object is destroyed);
         =====================================================================*/
        private void shutDown()
        {
            if (dbConnection != null)
                dbConnection.Close();
        }

        /*=====================================================================
         * Initial setup of tables in the database
         =====================================================================*/
        private void setUpTables()
        {
            dbConnection.CreateTable<Score>(); // example table being created
        }
        /*=====================================================================
         * Initial connection to the database
         =====================================================================*/
        private void setUpDB()
        {
          //get the path to where the application can store internal data 
          string folderPath = System.Environment.GetFolderPath( System.Environment.SpecialFolder.ApplicationData );
          string dbFileName = "AppData.db"; // name we want to give to our db file
          string fullDBPath = System.IO.Path.Combine(folderPath,dbFileName); // properly formate the path for the system we are on

          //if file does not already exist it will be created for us
          dbConnection = new SQLiteConnection(fullDBPath);
          setUpTables(); // this happens very time.
        }

        public void addScore(Score info)
        {
            dbConnection.Insert(info);
        }

        public Score getScoreByID(int id)
        {
            return dbConnection.Get<Score>(id);
        }

        public void deleteScoreByID(int id)
        {
            dbConnection.Delete<Score>(id);
        }

        public void updateScoreInfo(Score info)
        {
            dbConnection.Update(info);
        }

        public List<Score> getAllScores()
        {
            //gets all elements in the Score table and packages it into a List
            return new List<Score>(dbConnection.Table<Score>());
        }


        public List<Score> getAllScoresOrdered()
        {
            //gets all elements in the Score table and packages it into a List
            return new List<Score>(dbConnection.Table<Score>().OrderByDescending(st => st.score));
        }
    }
}