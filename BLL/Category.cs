// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ApnaPages
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Reflection;
    

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 
    public class Category
           {
               string name = "";
               object value = null;

               public Category(string name, object value)
               {
                   this.name = name;
                   this.value = value;
               }

               public string Name { get { return name; } set { name = value; } }
               public object Value { get { return value; } set { this.value = value; } }

               public override string ToString()
               {
                   return name;
               }
           }

    public class Categories
    {


       
        public static List<Category> getAllCategories()
        {
            List<Category> catList = new List<Category>();

            foreach (CategoriesTypes cat in EnumToList<CategoriesTypes>())
            {
                GetEnumDescription(cat);
                Category category= new Category(GetEnumDescription(cat),cat);
                catList.Add(category);
            }
            return catList;
        }

        public static int GetEnumValueFromDescription(string description)
        {
            string enumString = Categories.GetEnumName(typeof(Categories.CategoriesTypes), description);
            Type enumType = typeof(Categories.CategoriesTypes);
            FieldInfo enumItem1 = enumType.GetField(enumString);
            if (enumItem1 == null)
                return -1;
            int enumValue1 = (int)enumItem1.GetValue(enumType);

            return enumValue1;
        }

        public static string GetEnumName(System.Type value, string description)
        {
            FieldInfo[] fis = value.GetFields();
            foreach (FieldInfo fi in fis)
            {
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes
                  (typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    if (attributes[0].Description == description)
                    {
                        return fi.Name;

                    }
                }
            }
            return description;
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (name==value)
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        public enum States
        {
            California,
            NewMexico,
            NewYork,
            SouthCarolina,
            Tennessee,
            Washington
        }


        public enum CategoriesTypes
        {

            [Description("Accounting CA")]
            AccountingCA = 0,
            [Description("Accounting CGA")]
            AccountingCGA,
            [Description("Accounting General")]
            AccountingGeneral,
            [Description("Animal Hospital")]
            AnimalHospital,
            Appliances,
            Architect,
            Astrology,
            [Description("Auto Body")]
            AutoBody,
            [Description("Auto Electric")]
            AutoElectric,
            [Description("Auto Glass")]
            AutoGlass,
            [Description("Auto Parts")]
            AutoParts,
            [Description("Auto Radiator")]
            AutoRadiator,
            [Description("Auto Repair")]
            AutoRepair,
            [Description("Auto Sale")]
            AutoSale,
            Awning,
            Bakery,
            [Description("Banquet Hall")]
            BanquetHall,
            Beauty,
            Blinds,
            Builders,
            [Description("Building Supplies")]
            BuildingSupplies,
            Cabinets,
            [Description("Car Rental")]
            CarRental,
            [Description("Carpet Cleaning")]
            CarpetCleaning,
            [Description("Carpet Sale")]
            CarpetSale,
            Catering,
            [Description("Cell Phone")]
            CellPhone,
            Chiropractor,
            Closet,
            Computers,
            Concrete,
            [Description("Concrete Cutting")]
            ConcreteCutting,
            [Description("Counter Top")]
            CounterTop,
            Courier,
            Decoration,
            Dentist,
            Doors,
            Drainage,
            Drapery,
            [Description("Driving School")]
            DrivingSchool,
            Drywall,
            Electrical,
            Engineering,
            [Description("Equipment Rental")]
            EquipmentRental,
            Excavating,
            Fencing,
            [Description("Finishing Carpentry")]
            FinishingCarpentry,
            Fireplace,
            Flooring,
            [Description("Form Rentals")]
            FormRentals,
            Framing,
            Funeral,
            Furnace,
            Furniture,
            [Description("Garage Doors")]
            GarageDoors,
            [Description("Garbage Removal")]
            GarbageRemoval,
            Glass,
            Granite,
            Grocery,
            Gutter,
            [Description("Gutter Cleaning")]
            GutterCleaning,
            Health,
            Heating,
            [Description("Home Design")]
            HomeDesign,
            [Description("Home Inspection")]
            HomeInspection,
            Homeopathy,
            Immigration,
            Insulation,
            Insurance,
            [Description("Interior Designer")]
            InteriorDesigner,
            Janitorial,
            Jewellers,
            Landscaping,
            Laser,
            Lawyers,
            Lighting,
            Limousine,
            Locksmith,
            Masonary,
            Meat,
            Media,
            [Description("Money Exchange")]
            MoneyExchange,
            Mortgage,
            Moulding,
            Movers,
            [Description("Notary Public")]
            NotaryPublic,
            Optical,
            Painters,
            [Description("Party Rental")]
            PartyRental,
            Paving,
            [Description("Pest Control")]
            PestControl,
            Photography,
            Physician,
            Pizza,
            Plumbing,
            [Description("Plumbing Supplies")]
            PlumbingSupplies,
            [Description("Pressure Wash")]
            PressureWash,
            Railing,
            [Description("Real Estate")]
            RealEstate,
            Rebar,
            Recycling,
            Refrigeration,
            Renovation,
            Restaurant,
            [Description("Restaurant Equipment")]
            RestaurantEquipment,
            Roofing,
            [Description("Roofing Supplies")]
            RoofingSupplies,
            [Description("Rubbish Removal")]
            RubbishRemoval,
            Security,
            [Description("Security Alarm")]
            SecurityAlarm,
            Sewing,
            [Description("Shower Doors")]
            ShowerDoors,
            Siding,
            Signs,
            Sprinkler,
            Stone,
            Stucco,
            Sundeck,
            Surveillance,
            Survey,
            [Description("Tent Rental")]
            TentRental,
            Tiles,
            [Description("Tiles Installation")]
            TilesInstallation,
            Tires,
            [Description("Toilet Rental")]
            ToiletRental,
            Towing,
            Translation,
            Travel,
            [Description("Tree Cutting")]
            TreeCutting,
            Trophy,
            [Description("Truck Repair")]
            TruckRepair,
            [Description("TV Repair")]
            TVRepair,
            Vaccum,
            [Description("Video Production")]
            VideoProduction,
            [Description("Video Store")]
            VideoStore,
            [Description("Web Design")]
            WebDesign,
            Wedding,
            Window,
            [Description("Window Screen")]
            WindowScreen,
            [Description("Wood Working")]
            WoodWorking,
            Yoga,
            Favorites=1000


        }
   
    }
}
