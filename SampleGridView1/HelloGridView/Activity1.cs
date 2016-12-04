using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Util;
using Android.Systems;
using Android.Text;
using Java.Lang;
using System.Timers;
using Android.Media;
using Android.Graphics;

namespace HelloGridView
{
    [Activity(Label = "30 Seconds of Color!", Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
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
        private int sec = 0;
        private System.Timers.Timer timer;
        private TextView timerText;
        public MediaPlayer player;
        public MediaPlayer MatchSound;
        public MediaPlayer failMatchSound;
        public MediaPlayer timerAlmostDoneSound;
        public MediaPlayer endOfGameSound;
        private TextView combo1;
        private TextView combo2;
        private TextView combo3;
        private TextView next;
        private Color red = new Color(Color.ParseColor("red"));
        private Color blue = new Color(Color.ParseColor("cyan"));
        private Color yellow = new Color(Color.ParseColor("yellow"));
        private Color green = new Color(Color.ParseColor("green"));
        private Color[] randomColor;
        private Random rng = new Random();
        protected override void OnCreate(Bundle bundle)
        {
            //basic start up creation
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            randomColor = new Color[] {red,blue,yellow,green };
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

            //music and sound effect players
            if (gameCntr.sound == true)
            {
                MatchSound = MediaPlayer.Create(this, Resource.Raw.success);
                failMatchSound = MediaPlayer.Create(this, Resource.Raw.gameSoundIncorrect);
                timerAlmostDoneSound = MediaPlayer.Create(this, Resource.Raw.finsError2);
                endOfGameSound = MediaPlayer.Create(this, Resource.Raw.shortBuzzer);
            }
            if (gameCntr.music == true)
            {
                player = MediaPlayer.Create(this, Resource.Raw.lightisgreen);
                player.Start();
            }//starts background music - NEEDS to react to options preferences

            gridAdapter = new ImageAdapter(this);
            gridview.Adapter = gridAdapter;
            combo1 = FindViewById<TextView>(HelloGridView.Resource.Id.combo1);
            combo2 = FindViewById<TextView>(HelloGridView.Resource.Id.combo2);
            combo3 = FindViewById<TextView>(HelloGridView.Resource.Id.combo3);
            next = FindViewById<TextView>(HelloGridView.Resource.Id.next);
            scoreBox = FindViewById<TextView>(HelloGridView.Resource.Id.scoreBox);
            matchBox = FindViewById<TextView>(HelloGridView.Resource.Id.matchBox);
            updatePattern();
            //on click effect
            gridview.ItemClick += Gridview_ItemClick;
            gameCntr.loadBoard();
            gameCntr.score = 0;
            if (gameCntr.sound == true)
            {
                MatchSound.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string printSec;
            sec++;
            int mSec = 30 - sec;
            printSec = (mSec < 10) ? "0" + mSec.ToString() : mSec.ToString();
            RunOnUiThread(() => { timerText.Text = "0:" + printSec; });
            if(sec == 2)
            {
                next.Alpha = 0;
            }
            if (sec == 25 || sec == 26 || sec == 27 || sec == 28 || sec == 29)
            {
                if (gameCntr.sound == true) { timerAlmostDoneSound.Start(); }
            }
            if (sec == 30) {
                if (gameCntr.sound == true) { endOfGameSound.Start(); }
                Intent startScore = new Intent(this, typeof(HelloGridView.EnterHighScore));
                startScore.PutExtra("score",String.ValueOf(gameCntr.score));
                StartActivity(startScore);
                if (gameCntr.music == true)
                {
                    player.Stop();
                }
                Finish();
            }//what to do when time elapses
        }

        private void updatePattern()
        {
            patternArray = gameCntr.getRandPattern();
            //trying to make the pattern words into specific colors 
            //BACKUP: var text = "Next Pattern: " + patternArray[0] + ", " + patternArray[1] + ", " + patternArray[2];
            /*      case 0: stringPatt[i] ="Blue"; break;
                    case 1: stringPatt[i] = "Green"; break;
                    case 2: stringPatt[i] = "Red"; break;
                    case 3: stringPatt[i] = "Yellow"; break;
            SpannableStringBuilder builder = new SpannableStringBuilder();

            SpannableString str1 = new SpannableString("Text1");
            str1.setSpan(new  (Color.RED), 0, str1.length(), 0);
            builder.append(str1);

            SpannableString str2 = new SpannableString(appMode.toString());
            str2.setSpan(new ForegroundColorSpan(Color.GREEN), 0, str2.length(), 0);
            builder.append(str2);

            var text = "Next Pattern: ";
            var div = "-";*/
            combo1.Text = patternArray[0];
            combo2.Text = patternArray[1];
            combo3.Text = patternArray[2];
            if (gameCntr.difficultyHard)
            {
                for(int i = 0; i < patternArray.Length; i++)
                {
                    combo1.SetTextColor(randomColor[rng.NextInt(randomColor.Length)]);
                    combo2.SetTextColor(randomColor[rng.NextInt(randomColor.Length)]);
                    combo3.SetTextColor(randomColor[rng.NextInt(randomColor.Length)]);
                }
            }
            else
            { 
                combo1.SetTextColor(getColors(patternArray[0]));
                combo2.SetTextColor(getColors(patternArray[1]));
                combo3.SetTextColor(getColors(patternArray[2]));
            }
        }
        public override void OnBackPressed()
        {
            if (gameCntr.music == true)
            {
                player.Stop();
            }
            timer.Stop();
            this.Finish();
        }
        private Color getColors(string colour)
        {
            if (colour == "Red")
            {
                return red;
            } else if (colour == "Green")
            {
                return green;
            } else if(colour == "Blue")
            {
                return blue;
            }else{
                return yellow;
            }
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

                        if (gameCntr.sound == true)
                        {
                            MatchSound.Start();
                        }
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
                        if (gameCntr.sound == true)
                        {
                            failMatchSound.Start();
                        }
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