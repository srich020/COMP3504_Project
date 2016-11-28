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
    [Activity(Label = "HIGH SCORES", Icon = "@drawable/icon")] 
    public class HomeScreen : Activity
    {
        private LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();
        private ListView studentListView;
        private ScoreAdapter stAdapter; // data adapter for stored students
        private Button MainMenu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.homeScreen);
            //avoid automaticaly appear of android keyboard when activitry starts
            Window.SetSoftInputMode(SoftInput.StateHidden); 
            loadViews();
            connectActions();
            if (Intent.GetStringExtra("score") != null)
            {
                addScoreFromIntent();
            }

        }

        private void addScoreFromIntent()
        {
            string sc = Intent.GetStringExtra("score");
            string name = Intent.GetStringExtra("name");
            int scor = int.Parse(sc);
            data.addScore(new Score(scor, name));
            stAdapter.NotifyDataSetChanged(); //sends signal to list that it should refresh the data 
        }

        private void loadViews()
        {
            studentListView = FindViewById<ListView>(Resource.Id.studentListView);
            MainMenu = FindViewById<Button>(Resource.Id.HSmenuButton);
            MainMenu.Click += MenuButton_Click;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainMenu));
            StartActivity(intent);
        }

        private void connectActions()
        {
            //set up addapter for list view 
            stAdapter = new ScoreAdapter(this);
            studentListView.Adapter = stAdapter;
            studentListView.FastScrollEnabled = true;
            studentListView.ItemClick += StudentListView_ItemClick;
            studentListView.ItemLongClick += StudentListView_ItemLongClick;
        }

        //Will just display an alert of all the student info
        private void StudentListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Score selected = stAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Score Info");
            dialog.SetMessage(selected.name);
            dialog.Show();

        }

        //Will ask if they want to remove the student
        private void StudentListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Score selectedSt = stAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Delete Score");
            dialog.SetMessage(selectedSt.name);
            dialog.SetPositiveButton("Delete", 
                (senderAlert,args) => { // action for this button
                    data.deleteScoreByID(selectedSt.ID);
                    stAdapter.NotifyDataSetChanged();
                    Toast.MakeText(this, "The high score has been deleted", ToastLength.Short).Show();
                   } 
                );
            dialog.SetNegativeButton("cancel", (senderAlert, args) => { });

            dialog.Show();
          

        }

        private void hideKeyBoard(View element)
        {
            InputMethodManager im = (InputMethodManager)GetSystemService(Context.InputMethodService);
            im.HideSoftInputFromWindow(element.WindowToken, 0);

        }
    }
}

