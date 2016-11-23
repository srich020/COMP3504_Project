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
    public class StudentAdapter : BaseAdapter<Student>
    {
        private LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();
        private Activity context;

        public StudentAdapter(Activity context)
        {
            this.context = context;
        }

        public override int Count
        {
            get
            {
                return data.getAllStudents().Count;
            }
        }

        public override Student this[int position]
        {
            get
            {
                return data.getAllStudentsOrdered().ElementAt<Student>(position);
            }
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Student st = this[position];

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1,null);

            TextView txt = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
            txt.Text = ""+st;//this will call the toString of the Student class
            return convertView;
        }
    }
}