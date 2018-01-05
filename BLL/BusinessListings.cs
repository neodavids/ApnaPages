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
using System.Xml.Linq;
using System.IO;


namespace ApnaPages
{
	public  class BusinessListings
	{


        public static List<Business> getBusinessesByFavorites(string [] favList, XDocument xmlDoc)
        {
            var listings = from customer in xmlDoc.Descendants("row")
                           where favList.Contains(customer.Element("BusinessID").Value)
                           select new Business
                           {
                               BusinessID = Convert.ToInt32(customer.Element("BusinessID").Value),
                               Name = customer.Element("Name").Value,
                               Category = (Categories.CategoriesTypes)(Convert.ToInt16(customer.Element("Category").Value)),
                               Address = customer.Element("Address").Value,
                               City = customer.Element("City").Value,
                               Province = customer.Element("Province").Value,
                               Postal = customer.Element("Postal").Value,
                               Phone = customer.Element("Phone").Value,
                               Website = customer.Element("Website").Value,
                               Email = customer.Element("Email").Value,
                               Click2Call = Convert.ToBoolean(customer.Element("Click2Call").Value),
                           };
            return listings.ToList();
        }

        public static List<Business> getBusinessesThatMatchName(string filter, XDocument xmlDoc)
        {
            var listings = from customer in xmlDoc.Descendants("row")
                           where customer.Element("Name").Value.ToUpper().Contains(filter.ToUpper())
                           select new Business
                           {
                               BusinessID = Convert.ToInt32(customer.Element("BusinessID").Value),
                               Name = customer.Element("Name").Value,
                               Category = (Categories.CategoriesTypes)(Convert.ToInt16(customer.Element("Category").Value)),
                               Address = customer.Element("Address").Value,
                               City = customer.Element("City").Value,
                               Province = customer.Element("Province").Value,
                               Postal = customer.Element("Postal").Value,
                               Phone = customer.Element("Phone").Value,
                               Website = customer.Element("Website").Value,
                               Email = customer.Element("Email").Value,
                               Click2Call = Convert.ToBoolean(customer.Element("Click2Call").Value),






                           };
            return listings.ToList();
        }

       public static List<Business> getBusinessesByCategory(short category, XDocument xmlDoc)
        {            
            var listings = from customer in xmlDoc.Descendants("row")
                           where customer.Element("Category").Value == category.ToString()
                           select new Business
                           {
                               BusinessID = Convert.ToInt32(customer.Element("BusinessID").Value),
                               Name = customer.Element("Name").Value,                            
                               Category = (Categories.CategoriesTypes)(Convert.ToInt16(customer.Element("Category").Value)),
                               Address =  customer.Element("Address").Value,
                               City = customer.Element("City").Value,   	
		                       Province =customer.Element("Province").Value,   	
		                       Postal =customer.Element("Postal").Value,   	
                               Phone =customer.Element("Phone").Value,   	
                               Website =customer.Element("Website").Value,   	
                               Email =customer.Element("Email").Value,   	
                               Click2Call =Convert.ToBoolean(customer.Element("Click2Call").Value),   	
		
                           };
            return listings.ToList();                      
        }

       public static Business getBusinessesByID(int id, XDocument xmlDoc)
       {
           var listings = from customer in xmlDoc.Descendants("row")
                          where customer.Element("BusinessID").Value == id.ToString()
                          select new Business
                          {
                              BusinessID = Convert.ToInt32(customer.Element("BusinessID").Value),
                              Name = customer.Element("Name").Value,
                              Category = (Categories.CategoriesTypes)(Convert.ToInt16(customer.Element("Category").Value)),
                              Address = customer.Element("Address").Value,
                              City = customer.Element("City").Value,
                              Province = customer.Element("Province").Value,
                              Postal = customer.Element("Postal").Value,
                              Phone = customer.Element("Phone").Value,
                              Website = customer.Element("Website").Value,
                              Email = customer.Element("Email").Value,
                              Click2Call = Convert.ToBoolean(customer.Element("Click2Call").Value),

                          };
           return listings.First();
       }
       
		
	}
}