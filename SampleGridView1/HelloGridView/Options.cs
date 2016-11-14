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

namespace Options
{
    [Activity(Label = "Options")]
    public class Options : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(HelloGridView.Resource.Layout.MainMenuLayout);
            // Create your application here
        }
    }
}