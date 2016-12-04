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

namespace HelloGridView
{
    public class ScoreAdapter : BaseAdapter<Score>
    {
        private LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();
        private Activity context;

        public ScoreAdapter(Activity context)
        {
            this.context = context;
        }

        public override int Count
        {
            get
            {
                return data.getAllScores().Count;
            }
        }

        public override Score this[int position]
        {
            get
            {
                return data.getAllScoresOrdered().ElementAt<Score>(position);
            }
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Score st = this[position];

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1,null);

            TextView txt = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
            txt.Text = ""+st;//this will call the toString of the Score class
            txt.Text += " - " + st.name;
            return convertView;
        }
    }
}