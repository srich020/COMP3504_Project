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
    [Activity(Label = "Instructions", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class instructions : Activity
    {
        private Button mainMenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.instructionsLayout);
            mainMenu = FindViewById<Button>(Resource.Id.instructionsMenu);
            mainMenu.Click += mainMenu_Click;
            // Create your application here
        }

        private void mainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainMenu));
            StartActivity(intent);
        }
    }
}