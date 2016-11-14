using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace HelloGridView
{
    public class ImageAdapter : BaseAdapter
    {
        private readonly Context context;
        public ImageAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get { return thumbIds.Length; }
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
                imageView.Id = thumbIds[position];
                imageView.SetTag(thumbIds[position],"blue");
                
            }
            else
            {
                imageView = (ImageView) convertView;
            }

            imageView.SetImageResource(thumbIds[position]);
            return imageView;
        }

        // references to our images
        private readonly int[] thumbIds = {
                                              Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                                              Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                              Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                                              Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                              Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                                              Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                              Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                                              Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                              Resource.Drawable.Blue_static, Resource.Drawable.Green_static,
                                              Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                               Resource.Drawable.Red_static, Resource.Drawable.Yellow_static,
                                                Resource.Drawable.Red_static, Resource.Drawable.Yellow_static

                                          };
    }
}