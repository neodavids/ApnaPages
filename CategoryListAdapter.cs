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
using System.Xml.Linq;



namespace ApnaPages
{
	public class CategoryListAdapter : BaseAdapter
	{
		Activity context;
        short category;
		
		public List<Business> items;

        public CategoryListAdapter(Activity context, short categoryValue, string searchFilter,XDocument xmlDoc) //We need a context to inflate our row view from
			: base()
		{
			this.context = context;
            this.category = categoryValue;

            if (category==-1)
                this.items = BusinessListings.getBusinessesThatMatchName(searchFilter, xmlDoc);
            else if (category==(short)Categories.CategoriesTypes.Favorites)
            {
                string[] favList = searchFilter.Split(',');
                this.items = BusinessListings.getBusinessesByFavorites(favList, xmlDoc);
            }
            else
                this.items = BusinessListings.getBusinessesByCategory(categoryValue, xmlDoc);
            
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

        public Business GetItemAtPosition(int position)
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
			var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.categoryListItem, parent, false)) as LinearLayout;
				
			//Find references to each subview in the list item's view
			var button = view.FindViewById(Resource.Id.Click2Call) as Button;
            
            var textTop = view.FindViewById(Resource.Id.textTop) as TextView;
            var textBottom = view.FindViewById(Resource.Id.textBottom) as TextView;
            var imageItem = view.FindViewById(Resource.Id.imageItem) as ImageView;
            imageItem.SetImageResource(Resource.Drawable.rightarrow);
			//Assign this item's values to the various subviews


            button.SetTag(Resource.Id.Click2Call, item.Phone);
            
            button.Click += (sender, e) =>
            {
                string tag = (string)button.GetTag(Resource.Id.Click2Call);                
                textTop.SetText(tag, TextView.BufferType.Normal);
                var callIntent = new Intent(Intent.ActionDial);
                callIntent.SetData(Android.Net.Uri.Parse("tel:+"+ tag));
                context.StartActivity(callIntent);                 
            };
            

            if (item.Click2Call)
            {
                button.Visibility = ViewStates.Visible;
                imageItem.Visibility = ViewStates.Gone;
            }


            else
            {
                button.Visibility = ViewStates.Gone ;
                imageItem.Visibility = ViewStates.Visible;
                            
            }
                
			textTop.SetText(item.Name, TextView.BufferType.Normal);
			textBottom.SetText(item.Address, TextView.BufferType.Normal);
			
			//Finally return the view
			return view;
		}
	}
}