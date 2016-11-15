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
        private GridView gridview;
        private ImageAdapter gridAdapter;

        private GameController gameCntr = GameController.getInstance();

        protected override void OnCreate(Bundle bundle)
        {
            //basic start up creation
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //initializes the gridview. Set the adapter to the customer ImageAdapter
            gridview = FindViewById<GridView>(Resource.Id.gridview);
            
            gridAdapter = new ImageAdapter(this);
            gridview.Adapter = gridAdapter;
            debug = FindViewById<TextView>(HelloGridView.Resource.Id.textview);
            //on click effect
            gridview.ItemClick += Gridview_ItemClick;
           
                gameCntr.loadBoard();
        }

       

        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           
            //sel.colorValue = Resource.Drawable.Blue_static;

            gameCntr.updateWorldState(e.Position);
           
            gridAdapter.NotifyDataSetChanged();
        }
    }
}