using Android.App;
using Android.OS;
using Android.Widget;

namespace HelloGridView
{
    [Activity(Label = "HelloGridView", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
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
         
        }
    }
}