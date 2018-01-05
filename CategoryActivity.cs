using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Xml.Linq;

namespace ApnaPages
{
	[Activity(Label = "")]
	public class CategoryActivity : Activity
	{
		CategoryListAdapter listAdapter;
        public CategoryActivity() { }
        public CategoryActivity(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }
        short categoryValue;
		protected override void OnCreate(Bundle bundle)
		{
            
			base.OnCreate(bundle);
			
			//Set the Activity's view to our list layout		
			SetContentView(Resource.Layout.categoryList);
             
            

            categoryValue = Convert.ToInt16(Intent.GetStringExtra("Category"));
            string searchFilter = Intent.GetStringExtra("SearchFilter");
            

            Stream mystream2 = Assets.Open("BusinessListings.xml");
            XDocument xt = XDocument.Load(mystream2);  

			//Create our adapter
            listAdapter = new CategoryListAdapter(this, categoryValue,searchFilter,xt);

			//Find the listview reference
			var listView = FindViewById<ListView>(Resource.Id.listView);
		
			//Hook up our adapter to our ListView
			listView.Adapter = listAdapter;
            listView.Clickable = true;

			//Wire up the click event
			listView.ItemClick += new EventHandler<ItemEventArgs>(listView_ItemClick);
            

           
            var btnDirectory = FindViewById<Button>(Resource.Id.btnDirectory);
            btnDirectory.Click += (sender, e) =>
            {
                StartActivity(typeof(ApnaPages.DirectoryActivity));
            };

            var btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnHome.Click += (sender, e) =>
            {
                StartActivity(typeof(ApnaPages.HomeActivity));
            };

            var btnFavorite = FindViewById<Button>(Resource.Id.btnFavorite);
            btnFavorite.Click += (sender, e) =>
            {
                ISharedPreferences pref = GetSharedPreferences("favoritesFile", FileCreationMode.Private);
                string val = pref.GetString("favoritesList", "");
                Toast.MakeText(Application, val, ToastLength.Short).Show();


                var targetActivity = new Intent(this, typeof(CategoryActivity));
                targetActivity.PutExtra("SearchFilter", val);
                targetActivity.PutExtra("Category", "1000");
                StartActivity(targetActivity);
            };
		}

		void listView_ItemClick(object sender, ItemEventArgs e)
		{
			//Get our item from the list adapter
			var item = this.listAdapter.GetItemAtPosition(e.Position);


            var targetActivity = new Intent(this, typeof(DetailsActivity));
            targetActivity.PutExtra("BusinessID", item.BusinessID.ToString());
            StartActivity(targetActivity);

            
		}
	}
}

