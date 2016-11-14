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

namespace MainMenu
{
    [Activity(Label = "MainMenu")]
    public class MainMenu : Activity
    {
        public Button PlayButton;
        public Button HighScoresButton;
        public Button OptionsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resources.Layout.MainMenuLayout);
            // Create your application here
            getViews();
        }

        private void getViews()
        {
            PlayButton = FindViewById<Button>(Resource.Id.playButton);
            HighScoresButton = FindViewById<Button>(Resource.Id.scoreButton);
            OptionsButton = FindViewById<Button>(Resource.Id.optionsButton);
        }
    }
}