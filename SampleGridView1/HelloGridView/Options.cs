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

namespace HelloGridView
{
    [Activity(Label = "HelloGridView")]
    public class Options : Activity
    {
        private CheckBox sound;
        private CheckBox music;
        private RadioButton standard;
        private RadioButton hard;
        private GameController game = GameController.getInstance();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.optionsLayout);
            SetViews();
            // Create your application here
            sound.Click += Sound_Click;
            music.Click += Music_Click;
            standard.Click += Standard_Click;
            hard.Click += Hard_Click;

        }

        private void Hard_Click(object sender, EventArgs e)
        {
            if(game.difficultyHard == false)
            {
                game.difficultyHard = true;
            }
        }

        private void Standard_Click(object sender, EventArgs e)
        {
            if(game.difficultyHard == true)
            {
                game.difficultyHard = false;
            }
        }

        private void Music_Click(object sender, EventArgs e)
        {
            if (game.music == false)
            {
                game.music = true;
            }
            else
            {
                game.music = false;
            }
            

        }

        private void Sound_Click(object sender, EventArgs e)
        {
            if (game.sound == false)
            {
                game.sound = true;
            }
            else
            {
                game.sound = false;
            }
        }

        private void SetViews()
        {
            sound = FindViewById<CheckBox>(HelloGridView.Resource.Id.sound);
            if(game.sound == false)
            {
                sound.Checked = false;
            }
            music = FindViewById<CheckBox>(HelloGridView.Resource.Id.music);
            if (game.music == false)
            {
                music.Checked = false;
            }
            standard = FindViewById<RadioButton>(HelloGridView.Resource.Id.standard);
            standard.Checked = true;
            hard = FindViewById<RadioButton>(HelloGridView.Resource.Id.hard);
        }
    }
}