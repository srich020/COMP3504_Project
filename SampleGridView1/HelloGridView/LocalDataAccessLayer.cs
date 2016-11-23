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
            dbConnection.CreateTable<Student>(); // example table being created
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

        public void addStudent(Student info)
        {
            dbConnection.Insert(info);
        }

        public Student getStudentByID(int id)
        {
            return dbConnection.Get<Student>(id);
        }

        public void deleteStudentByID(int id)
        {
            dbConnection.Delete<Student>(id);
        }

        public void updateStudentInfo(Student info)
        {
            dbConnection.Update(info);
        }

        public List<Student> getAllStudents()
        {
            //gets all elements in the Student table and packages it into a List
            return new List<Student>(dbConnection.Table<Student>());
        }


        public List<Student> getAllStudentsOrdered()
        {
            //gets all elements in the Student table and packages it into a List
            return new List<Student>(dbConnection.Table<Student>().OrderBy(st => st.name));
        }
    }
}