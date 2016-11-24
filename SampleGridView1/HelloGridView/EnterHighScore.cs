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
    [Activity(Label = "EnterHighScore")]
    public class EnterHighScore : Activity { 
    private TextView scoreText;
    private EditText nameText;
        private Button submit;
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.EnterName);
            loadViews();
            submit.Click += Submit_Click;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Intent startScore = new Intent(this, typeof(HelloGridView.HomeScreen));
            startScore.PutExtra("score", scoreText.Text);
            startScore.PutExtra("name", nameText.Text);
            StartActivity(startScore);
            Finish();
        }

        private void loadViews()
        {
            scoreText = FindViewById<TextView>(Resource.Id.Score);
            scoreText.Text = Intent.GetStringExtra("score");
            nameText = FindViewById<EditText>(Resource.Id.Name);
            submit = FindViewById<Button>(Resource.Id.ScoreSubmit);
        }
    }
}