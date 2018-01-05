
using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ApnaPages
{
    [Activity(Label = "Directory")]
    public class DirectoryActivity : Activity
    {
        DirectoryListAdapter listAdapter;
        public DirectoryActivity() { }
        public DirectoryActivity(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			//Set the Activity's view to our list layout		
			SetContentView(Resource.Layout.directoryList);
						
			//Create our adapter
			listAdapter = new DirectoryListAdapter(this);

			//Find the listview reference
			var listView = FindViewById<ListView>(Resource.Id.listView);
		
			//Hook up our adapter to our ListView
			listView.Adapter = listAdapter;

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
                Toast.MakeText(Application, "Favorites " + val, ToastLength.Short).Show();
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

            

            var targetActivity = new Intent(this, typeof(CategoryActivity));
            targetActivity.PutExtra("Category", Convert.ToInt16(item.Value).ToString());
            StartActivity(targetActivity);

            
		}
	}
}

