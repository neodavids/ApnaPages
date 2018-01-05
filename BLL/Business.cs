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
	public class Business
	{

        public int BusinessID
        {
            get;
            set;
        }
		public string Name
		{
			get;
			set;
		}				

        public  Categories.CategoriesTypes Category
		{
			get;
			set;
		}
        public string Address
		{
			get;
			set;
		}
        public string City
		{
			get;
			set;
		}
        public string Province
		{
			get;
			set;
		}
        public string Postal
		{
			get;
			set;
		}
         public string Phone
		{
			get;
			set;
		}
         public string Website
		{
			get;
			set;
		}
        public string Email
		{
			get;
			set;
		}
        public bool Click2Call
		{
			get;
			set;
		}
        
        public string getEmailBody()
        {
            string s = "I would like to share this information with you:";
            s+= "\n\n" + this.Name;
            s+= "\n" + this.Address;
            s+= "\n" + this.City + this.Province;
            s+= "\n" + this.Phone;
            s+= "\n" + this.Website;

            s+= "\n\nListings provided by Appna Pages";            
            s+= "\n\nGet your very own AppnaPages.com mobile app. Visit";
            s+= "\n\nhttp://www.appnapages.com";

            return s;

        }


	}
}