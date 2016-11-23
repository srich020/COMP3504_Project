using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views.InputMethods;

namespace HelloGridView
{
    [Activity(Label = "Data Example", Icon = "@drawable/icon")] 
    public class HomeScreen : Activity
    {
        private LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();
        private ListView studentListView;
        private StudentAdapter stAdapter; // data adapter for stored students

        private Button addStudentButton;
        private EditText studentNameEditText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.homeScreen);
            //avoid automaticaly appear of android keyboard when activitry starts
            Window.SetSoftInputMode(SoftInput.StateHidden); 


            loadViews();
            connectActions();
        }


        private void loadViews()
        {
            studentListView = FindViewById<ListView>(Resource.Id.studentListView);
            addStudentButton = FindViewById<Button>(Resource.Id.addStudentButton);
            studentNameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
        }

        private void connectActions()
        {
            addStudentButton.Click += AddStudentButton_Click;

            //set up addapter for list view 
            stAdapter = new StudentAdapter(this);
            studentListView.Adapter = stAdapter;
            studentListView.FastScrollEnabled = true;

            studentListView.ItemClick += StudentListView_ItemClick;
            studentListView.ItemLongClick += StudentListView_ItemLongClick;
        }

        //Will just display an alert of all the student info
        private void StudentListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Student selectedSt = stAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Student Info");
            dialog.SetMessage(selectedSt.ID + " " + selectedSt.name);
            dialog.Show();

        }

        //Will ask if they want to remove the student
        private void StudentListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Student selectedSt = stAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Delete Student");
            dialog.SetMessage(selectedSt.ID + " " + selectedSt.name);
            dialog.SetPositiveButton("Delete", 
                (senderAlert,args) => { // action for this button
                    data.deleteStudentByID(selectedSt.ID);
                    stAdapter.NotifyDataSetChanged();
                    Toast.MakeText(this, "The student has been deleted", ToastLength.Short).Show();
                   } 
                );
            dialog.SetNegativeButton("cancel", (senderAlert, args) => { });

            dialog.Show();
          

        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            data.addStudent(new Student(studentNameEditText.Text));
            studentNameEditText.Text = ""; //clear field after entering data
            stAdapter.NotifyDataSetChanged(); //sends signal to list that it should refresh the data 

            //Hide keyboard after use for our text field
            hideKeyBoard(studentNameEditText);

        }


        private void hideKeyBoard(View element)
        {
            InputMethodManager im = (InputMethodManager)GetSystemService(Context.InputMethodService);
            im.HideSoftInputFromWindow(element.WindowToken, 0);

        }
    }
}

