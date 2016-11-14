﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;

namespace HelloGridView
{
    [Activity(Label = "HelloGridView", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 0;
        public ArrayList myArray = new ArrayList();

        protected override void OnCreate(Bundle bundle)
        {
            //basic start up creation
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //initializes the gridview. Set the adapter to the customer ImageAdapter
            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            gridview.Adapter = new ImageAdapter(this);

            //on click effect
            gridview.ItemClick += (sender, args) => Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            gridview.ItemClick += Gridview_ItemClick;

        }

        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            count++;
            if (count < 3)
            {
                string dude = sender.ToString();
                myArray.Add(dude);
            }else
            {
                    var activity2 = new Intent(this, typeof(Activity2));
                    activity2.PutExtra("one","It somewhat works");
                     StartActivity(activity2);
                    count = 0;
            }
        }
    }
}