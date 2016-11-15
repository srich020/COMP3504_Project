using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace HelloGridView
{
    public class ImageAdapter : BaseAdapter
    {

        private GameController gameCntr = GameController.getInstance();

        private readonly Context context;
        public ImageAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get { return gameCntr.length(); }
        }

        public override Object GetItem(int position)
        {
            return null;
        }

      public override long GetItemId(int position)
      {
          return 0;
      }

        // create a new ImageView for each item referenced by the Adapter
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;

            if (convertView == null)
            {
                // if it's not recycled, initialize some attributes
                imageView = new ImageView(context);
                imageView.LayoutParameters = new AbsListView.LayoutParams(100, 100);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop); //ensures images stay square
               imageView.SetPadding(1, 1, 1, 1); //padding around, but will stretch to fill the whole screen
               
                
            }
            else
            {
                imageView = (ImageView) convertView;
            }

            ColorSquare sel = gameCntr.get(position);
            imageView.SetImageResource(sel.showColor());
            return imageView;
        }

      
    }
}