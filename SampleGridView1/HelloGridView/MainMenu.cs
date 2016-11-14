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

namespace MainMenu
{
    [Activity(Label = "MainMenu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainMenu : Activity
    {
        public Button PlayButton;
        public Button HighScoresButton;
        public Button OptionsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.MainMenuLayout);
            getViews();
            PlayButton.Click += PlayButton_Click;
            OptionsButton.Click += OptionsButton_Click;
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
            //HighScoresButton = FindViewById<Button>(Resource.Id.scoreButton);
            OptionsButton = FindViewById<Button>(HelloGridView.Resource.Id.optionsButton);
        }
    }
}