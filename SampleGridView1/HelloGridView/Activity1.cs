using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;
using Java.Lang;

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
        private int[] selectedValues = new int[3];
        private ColorSquare[] selectedSquares = new ColorSquare[3];
        private GameController gameCntr = GameController.getInstance();
        private int score;

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
            score = 0;
        }

        private void updatePattern()
        {
            patternArray = gameCntr.getRandPattern();
            nextPattern.Text = "Next Pattern: " + patternArray[0] + ", " + patternArray[1] + ", " + patternArray[2];
        }

        private bool isSelected(ColorSquare square)
        {
            bool isSelect = false;
                for (int i = 0; i < selectedSquares.Length; i++)
                {
                if (selectedSquares[i] != null && selectedSquares[i].xLoc == square.xLoc && selectedSquares[i].yLoc == square.xLoc)
                    {
                    
                        isSelect = true;
                        comboBox.Text = "Already Selected!";
                    }
            }
            comboBox.Text = "Boolean activated";
            return isSelect;
        }


        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            gameCntr.updateWorldState(e.Position);
            gridAdapter.NotifyDataSetChanged();
            ColorSquare selectedSquare = gameCntr.get(e.Position);
            
            string[] stringCompPatt = new string[3];
            //score and match processing
            
            if (count < 3)
            {
                if (isSelected(selectedSquare))
                {
                    comboBox.Text = "Not correct";
                }
                else
                {
                    int selNum = selectedSquare.colorNum;
                    selectedValues[count] = selNum;
                    selectedSquares[count] = selectedSquare;
                    count++;
                }
                if (count == 3)
                {//if three selected total, process score
                    
                    //create a string list to compare from the selected values
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
                    //if the selected three match the pattern given, process score and adjust board as needed
                    if (stringCompPatt[0]==patternArray[0] && stringCompPatt[1] == patternArray[1] && stringCompPatt[2] == patternArray[2])
                    {
                        //needs more checking to ensure same row or column for matches
                        comboBox.Text = "MATCHED MOTHERFUCKER!!!!!";
                        score++;
                        scoreBox.Text = "Score: "+score;
                        //reset all selected values AND inbetween squares (to do)
                        gameCntr.processMatch(selectedSquares);
                        selectedSquares = new ColorSquare[3];
                        selectedValues = new int[3];
                        count = 0;
                        updatePattern();
                    }
                    else 
                    {//the match wasn't corrent
                        comboBox.Text = "No match, you suck";
                        gameCntr.deToggleAll();
                    }
                    
                    count = 0;
                }
            }
            else
            {
                //will never happen
            }
        }
    }
}