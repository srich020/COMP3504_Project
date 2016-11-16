using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;

namespace HelloGridView
{
    [Activity(Label = "HelloGridView", Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 0;
        public string put = "";
        public TextView nextPattern = null;
        private TextView scoreBox;
        private TextView comboBox;
        private GridView gridview;
        private ImageAdapter gridAdapter;
        private string[] patternArray;
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
            nextPattern = FindViewById<TextView>(HelloGridView.Resource.Id.NextPattern);
            scoreBox = FindViewById<TextView>(HelloGridView.Resource.Id.scoreBox);
            comboBox = FindViewById<TextView>(HelloGridView.Resource.Id.comboBox);
            updatePattern();
            //on click effect
            gridview.ItemClick += Gridview_ItemClick;
            gameCntr.loadBoard();
        }

        private void updatePattern()
        {
            patternArray = gameCntr.getRandPattern();
            nextPattern.Text = "Next Pattern: " + patternArray[0] + ", " + patternArray[1] + ", " + patternArray[2];
        }

        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            gameCntr.updateWorldState(e.Position);
            gridAdapter.NotifyDataSetChanged();
            ColorSquare selectedSquare = gameCntr.get(e.Position);

            int[] selectedValues = new int[3];
            string[] stringCompPatt = new string[3];
            //score and match processing
            
            if (count < 3)
            {
                int selNum = selectedSquare.colorNum;
                comboBox.Text = "Selected: " + selNum;
                selectedValues[count] = selNum;
                count++;
            }
            else
            {
                //if (patternArray[0]==selectedValues[0])
                count++;
                //int selNum = selectedSquare.colorNum;
                //selectedValues[count] = selNum;
                comboBox.Text = "else loop";
                for (int i = 0; i < selectedValues.Length; i++)
                {
                    switch (selectedValues[i])
                    {
                        case 0: stringCompPatt[i] = "Blue"; break;
                        case 1: stringCompPatt[i] = "Green"; break;
                        case 2: stringCompPatt[i] = "Red"; break;
                        case 3: stringCompPatt[i] = "Yellow"; break;
                    }
                }
                scoreBox.Text = "3 clicked! " + stringCompPatt[0] + ", " + stringCompPatt[1] + ", " + stringCompPatt[2];
                count = 0;
            }
        }
    }
}