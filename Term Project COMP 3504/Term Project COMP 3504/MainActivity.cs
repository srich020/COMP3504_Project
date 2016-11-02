using Android.App;
using Android.Widget;
using Android.OS;

namespace Term_Project_COMP_3504
{
    [Activity(Label = "Term_Project_COMP_3504", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {


            SetContentView(Resource.Layout.Main);

            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            gridview.Adapter = new ImageAdapter(this);

            gridview.ItemClick += (sender, args) => Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
        }
    }
}

