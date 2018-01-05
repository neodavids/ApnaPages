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

namespace ApnaPages
{
    [Activity(Label = "Apna Pages")]
    public class HomeActivity : Activity
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);

            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView);            
            string[] businessNames = Resources.GetStringArray(Resource.Array.businessNames);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.AutoComplete_list_item, businessNames);
            textView.Adapter = adapter;
            
           Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
           spinner.ItemSelected += new EventHandler<ItemEventArgs>(spinner_ItemSelected);
           var adapterSpinner = ArrayAdapter.CreateFromResource(
               this, Resource.Array.locations_array, Android.Resource.Layout.SimpleSpinnerItem);
           adapterSpinner.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
           spinner.Adapter = adapterSpinner;


           
           
           var btnFind = FindViewById<Button>(Resource.Id.btnFind);
           btnFind.Click += (sender, e) =>
           {
               if (textView.Text != "")
               {
                   string category = Categories.GetEnumValueFromDescription(textView.Text).ToString();        
               var targetActivity = new Intent(this, typeof(CategoryActivity));
                   targetActivity.PutExtra("SearchFilter", textView.Text);
                   targetActivity.PutExtra("Category",category);
                 StartActivity(targetActivity);
               }                         
           };
            var btnDirectory = FindViewById<Button> (Resource.Id.btnDirectory);
            btnDirectory.Click += (sender, e) =>
            {
                StartActivity (typeof(ApnaPages.DirectoryActivity));
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


        private void spinner_ItemSelected(object sender, ItemEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

          
        }
      
    }

}


