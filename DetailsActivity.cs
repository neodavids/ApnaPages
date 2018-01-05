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
using System.IO;
using System.Xml.Linq;
using Android.Telephony;

namespace ApnaPages
{
      [Activity(Label = "Details")]
    public class DetailsActivity : Activity
    {              
        private const int DIALOG_LIST = 1;
        private Business business;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.details);

            Stream mystream2 = Assets.Open("BusinessListings.xml");
            XDocument xt = XDocument.Load(mystream2);
            int businessID = Convert.ToInt32(Intent.GetStringExtra("BusinessID"));
             business = BusinessListings.getBusinessesByID(businessID,xt);

            var txtName = FindViewById<TextView>(Resource.Id.textName);
            txtName.Text = business.Name;
            var txtAddress = FindViewById<TextView>(Resource.Id.textAddress);
            txtAddress.Text = business.Address;
            var txtPhone = FindViewById<TextView>(Resource.Id.textPhone);
            txtPhone.Text = business.Phone;
            var txtCity = FindViewById<TextView>(Resource.Id.textCity);
            txtCity.Text = business.City + ", " + business.Province + ", " + business.Postal;
            var txtWebsite = FindViewById<TextView>(Resource.Id.textWebsite);
            txtWebsite.Text = business.Website;

            var btnAddToFavorites = FindViewById<Button>(Resource.Id.btnAddToFavorites);
            ISharedPreferences prefFav = GetSharedPreferences("favoritesFile", FileCreationMode.Private);
            string favValue = prefFav.GetString("favoritesList", "");
            if (favValue.Contains("," + businessID + ",") == false)
            {
                btnAddToFavorites.SetBackgroundResource(Resource.Drawable.btn_green_matte);
                btnAddToFavorites.SetText(Resource.String.Add);
            }
            else
            {
                btnAddToFavorites.SetBackgroundResource(Resource.Drawable.btn_red_matte);
                btnAddToFavorites.SetText(Resource.String.Del);
            }
#if DEBUG
            business.Phone  = "5556";
#endif



            
           
            btnAddToFavorites.Click += (sender, e) =>
            {
                


                ISharedPreferences pref =  GetSharedPreferences("favoritesFile", FileCreationMode.Private);
                string value = pref.GetString("favoritesList", "");

                if (value.Contains("," + business.BusinessID + ","))
                {                  
                    value += businessID + ",";
                    ISharedPreferencesEditor ePref = pref.Edit();
                    ePref.PutString("favoritesList", value);
                    ePref.Commit();
                    var btnAdd = FindViewById<Button>(Resource.Id.btnAddToFavorites);
                    btnAdd.SetBackgroundResource(Resource.Drawable.btn_green_matte);
                    btnAdd.SetText(Resource.String.Add);
                    Toast.MakeText(Application, "Removed From Favorites " + value, ToastLength.Short).Show();
                }
                else
                {
                    value = value.Replace("," + business.BusinessID, "");                    
                    ISharedPreferencesEditor ePref = pref.Edit();
                    ePref.PutString("favoritesList", value);
                    ePref.Commit();
                    var btnAdd = FindViewById<Button>(Resource.Id.btnAddToFavorites);
                    btnAdd.SetBackgroundResource(Resource.Drawable.btn_red_matte);
                    btnAdd.SetText(Resource.String.Del);
                    Toast.MakeText(Application, "Added To Favorites " + value, ToastLength.Short).Show();

                }
            };
          

            var btnMap = FindViewById<Button>(Resource.Id.btnMap);
            btnMap.Click += (sender, e) =>
            {
                string  url = business.Address + "+" + business.City + "+" + business.Postal + "+" + business.Province;
                var geoUri = Android.Net.Uri.Parse("geo:0,0?q=" + url);                
                var mapIntent = new Intent(Intent.ActionView, geoUri);
                StartActivity(mapIntent);
            };
          

            var btnSMS = FindViewById<Button>(Resource.Id.btnSMS);
            btnSMS.Click += (sender, e) =>
            {
                               
                StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.FromParts("sms", business.Phone , null)));
            };

            var btnCall = FindViewById<Button>(Resource.Id.btnCall);
            btnCall.Click += (sender, e) =>
            {
                var callIntent = new Intent(Intent.ActionDial);
                callIntent.SetData(Android.Net.Uri.Parse("tel:+"+ business.Phone));
                StartActivity(callIntent);
            };

            var btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnHome.Click += (sender, e) =>
            {
                StartActivity(typeof(ApnaPages.HomeActivity));
            };
            var btnDirectory = FindViewById<Button>(Resource.Id.btnDirectory);
            btnDirectory.Click += (sender, e) =>
            {
                StartActivity(typeof(ApnaPages.DirectoryActivity));
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

            var btnShare = FindViewById<Button>(Resource.Id.btnShare);
            btnShare.Click += (sender, e) =>
            {
                ShowDialog(DIALOG_LIST);
                
            };
         



        }

        protected override Dialog OnCreateDialog(int id)
        {

            
            switch (id)
            {
                case DIALOG_LIST:
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Share");
                        builder.SetItems(Resource.Array.shareDialogList, ListClicked);

                        return builder.Create();
                    }
             
            }
            return null;
        }


        private void ListClicked(object sender, DialogClickEventArgs e)
        {
            var items = Resources.GetStringArray(Resource.Array.shareDialogList);
           
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(string.Format("You selected: {0} , {1}", (int)e.Which, items[(int)e.Which]));

            if ((int)e.Which==0)
                StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.FromParts("sms", business.Phone, null)));
            else if ((int)e.Which == 1)
            { 
               
                Intent i = new Intent(Intent.ActionSend);
                i.SetType("text/plain");               
                i.PutExtra(Intent.ExtraSubject, business.Name +  " from AppnaPages.com");
                i.PutExtra(Intent.ExtraText, business.getEmailBody());
                StartActivity(Intent.CreateChooser(i, "Send mail..."));
                

            }
        }

       


    }
}