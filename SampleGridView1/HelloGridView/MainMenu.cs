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

namespace HelloGridView
{
    [Activity(Label = "MainMenu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainMenu : Activity
    {
        public Button PlayButton;
        public Button HighScoresButton;
        public Button OptionsButton;
        public Button instructionsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.MainMenuLayout);
            getViews();
            PlayButton.Click += PlayButton_Click;
            OptionsButton.Click += OptionsButton_Click;
            HighScoresButton.Click += HighScoresButton_Click;
            instructionsButton.Click += InstructionsButton_Click;
        }

        private void InstructionsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(instructions));
            StartActivity(intent);
        }

        private void HighScoresButton_Click(object sender, EventArgs e)
        {
            var game = new Intent(this, typeof(HelloGridView.HomeScreen));
            StartActivity(game);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            var game = new Intent(this, typeof(HelloGridView.Activity1));
            StartActivity(game);
        }
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(HelloGridView.Options)));
        }

        private void getViews()
        {
            PlayButton = FindViewById<Button>(HelloGridView.Resource.Id.playButton);
            HighScoresButton = FindViewById<Button>(HelloGridView.Resource.Id.scoreButton);
            OptionsButton = FindViewById<Button>(HelloGridView.Resource.Id.optionsButton);
            instructionsButton = FindViewById<Button>(Resource.Id.instructionsButton);
        }
    }
}