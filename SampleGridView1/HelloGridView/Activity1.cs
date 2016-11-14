using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;

namespace HelloGridView
{
    [Activity(Label = "HelloGridView",Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 0;
        public string put = "";
        public TextView debug = null;

        protected override void OnCreate(Bundle bundle)
        {
            //basic start up creation
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //initializes the gridview. Set the adapter to the customer ImageAdapter
            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            gridview.Adapter = new ImageAdapter(this);
            debug = FindViewById<TextView>(HelloGridView.Resource.Id.textview);
            //on click effect
            gridview.ItemClick += Gridview_ItemClick;
            
        }

        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            count++;
            if (count < 3)
            {
                put += e.Position.ToString() + "," + e.Id;
            }else
            {
                put += e.Position.ToString() + "," + e.Id;
                debug.Text = put;
                    count = 0;
                put = "";
            }
        }
    }
}