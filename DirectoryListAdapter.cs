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
using Android.Graphics.Drawables;

namespace ApnaPages
{
	public class DirectoryListAdapter : BaseAdapter
	{
			
		Activity context;
		
		public List<Category> items;

        public DirectoryListAdapter(Activity context) //We need a context to inflate our row view from
			: base()
		{
			this.context = context;
            this.items = Categories.getAllCategories();
		}

		public override int Count
		{
			get { return items.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return position;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

        public Category GetItemAtPosition(int position)
		{
			return items[position];
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			//Get our object for this position
			var item = items[position];			
			
			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// This gives us some performance gains by not always inflating a new view
			// This will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.directoryListItem, parent, false)) as LinearLayout;
				
			//Find references to each subview in the list item's view
			var imageItem = view.FindViewById(Resource.Id.imageItem) as ImageView;
            var textTop = view.FindViewById(Resource.Id.textTop) as TextView;
            //var textBottom = view.FindViewById(Resource.Id.textBottom) as TextView;

			//Assign this item's values to the various subviews
			imageItem.SetImageResource(Resource.Drawable.rightarrow);
			textTop.SetText(item.Name, TextView.BufferType.Normal);
			//textBottom.SetText(item.Description, TextView.BufferType.Normal);

            
                
			//Finally return the view
			return view;
		}
	}
}