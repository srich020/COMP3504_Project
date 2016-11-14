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
using Android;
using Java.Lang;

namespace MainMenu
{
    [Activity(Label = "MainMenu", MainLauncher = true)]
    public class MainMenu : Activity
    {
        public Button PlayButton;
        public Button HighScoresButton;
        public Button OptionsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.MainMenuLayout);
            // Create your application here
            getViews();
            PlayButton.Click += PlayButton_Click;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            var game = new Intent(this, typeof(HelloGridView.Activity1));
            StartActivity(game);
        }

        private void getViews()
        {
            PlayButton = FindViewById<Button>(HelloGridView.Resource.Id.playButton);
            //HighScoresButton = FindViewById<Button>(HelloGridView.Resource.Id.scoreButton);
            //OptionsButton = FindViewById<Button>(HelloGridView.Resource.Id.optionsButton);
        }
    }
}