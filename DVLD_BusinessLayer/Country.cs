using System;
using System.Data;
using DVLD_DataAccessLayer;


namespace DVLD_BusinessLayer
{
    public class Country
    {
        public int countryID { set; get; }
        public string countryName { set; get; }



        private Country(int countryID,string countryName)
        {
            this.countryID = countryID;
            this.countryName = countryName;

        }

       public static DataTable getCountryRecord()
        {
            DataTable dt = CountriesDataAccess.getCountryRecords();
            return dt;
        }

        public static Country findCountry(int countryID)
        {
            string countryName = "";

      


            if (CountriesDataAccess.findCountry(countryID, ref countryName))
            {
                return new Country(countryID, countryName);

            }
            return null;
        }

        public static bool isCountryExist(int countryID)
        {
            return CountriesDataAccess.isCountryExist(countryID);
        }
    }
}
