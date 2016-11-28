using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;
using Java.Lang;
using System.Timers;
using Android.Media;

namespace HelloGridView
{
    [Activity(Label = "30 Seconds of Color!", Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 0;
        public string put = "";
        public TextView nextPattern = null;
        private TextView scoreBox;
        private GridView gridview;
        private TextView matchBox;
        private ImageAdapter gridAdapter;
        private string[] patternArray;
        private int[] selectedValues = new int[3];
        private ColorSquare[] selectedSquares = new ColorSquare[3];
        private GameController gameCntr = GameController.getInstance();
        private int score;
        private int sec = 0;
        private System.Timers.Timer timer;
        private TextView timerText;
        public MediaPlayer player;
        public MediaPlayer MatchSound;

        protected override void OnCreate(Bundle bundle)
        {
            //basic start up creation
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //initializes the gridview. Set the adapter to the customer ImageAdapter
            gridview = FindViewById<GridView>(Resource.Id.gridview);
            //timer things
            timerText = FindViewById<TextView>(Resource.Id.timerText);
            timer = new System.Timers.Timer();
            timer.Interval = 1000; //1 s interval
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            //end timer things
            player = MediaPlayer.Create(this, Resource.Raw.lightisgreen);
            //MatchSound = MediaPlayer.Create(this, Resource.Raw);
            player.Start();

            gridAdapter = new ImageAdapter(this);
            gridview.Adapter = gridAdapter;
            nextPattern = FindViewById<TextView>(HelloGridView.Resource.Id.NextPattern);
            scoreBox = FindViewById<TextView>(HelloGridView.Resource.Id.scoreBox);
            matchBox = FindViewById<TextView>(HelloGridView.Resource.Id.matchBox);
            updatePattern();
            //on click effect
            gridview.ItemClick += Gridview_ItemClick;
            gameCntr.loadBoard();
            gameCntr.score = 0;
            score = 0;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string printSec;
            sec++;
            int mSec = 30 - sec;
            printSec = (mSec < 10) ? "0" + mSec.ToString() : mSec.ToString();
            RunOnUiThread(() => { timerText.Text = "0:" + printSec; });
            if (sec == 30) {
                Intent startScore = new Intent(this, typeof(HelloGridView.EnterHighScore));
                startScore.PutExtra("score",String.ValueOf(gameCntr.score));
                StartActivity(startScore);
                player.Stop();
                Finish();
            }//what to do when time elapses
        }

        private void updatePattern()
        {
            patternArray = gameCntr.getRandPattern();
            nextPattern.Text = "Next Pattern: " + patternArray[0] + ", " + patternArray[1] + ", " + patternArray[2];
        }

   

        //Doesnt work properly yet
        private bool isSelected(ColorSquare square)
        {
            bool isSelect = false;
                for (int i = 0; i < selectedSquares.Length; i++)
                {
                if (selectedSquares[i] != null && selectedSquares[i].xLoc == square.xLoc && selectedSquares[i].yLoc == square.xLoc)
                    {
                    scoreBox.Text = "Already Selected!";
                    isSelect = true;
                        
                    }
                }
            
            return isSelect;
        }


        private void Gridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            gameCntr.updateWorldState(e.Position);
            gridAdapter.NotifyDataSetChanged();
            ColorSquare selectedSquare = gameCntr.get(e.Position);
            
            string[] stringCompPatt = new string[3]; //only used when matching 3 selected. converts values to strings for comparison
            //score and match processing
            
            if (count < 3)
            {

                
                int selNum = selectedSquare.colorNum;
                selectedValues[count] = selNum;
                selectedSquares[count] = selectedSquare;
                count++;

                /*if (!selectedSquare.selected)
                {
                    
                }*/

                if (count == 3) //AND add row checking logic
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
                    if (stringCompPatt[0]==patternArray[0] && stringCompPatt[1] == patternArray[1] && stringCompPatt[2] == patternArray[2] && gameCntr.processMatch(selectedSquares))
                    {


                        matchBox.Text = "MATCH!";
                        scoreBox.Text = "Score: "+gameCntr.score;
                        //reset all selected values AND inbetween squares (to do)
                        
                        selectedSquares = new ColorSquare[3];
                        selectedValues = new int[3];
                        count = 0;
                        updatePattern();
                    }
                    else 
                    {//the match wasn't corrent
                       
                        gameCntr.deToggleAll();
                        matchBox.Text = "No match, try again!";
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