using System;
using System.Data;
using DVLD_DataAccessLayer;


namespace DVLD_BusinessLayer
{
    public class clsCountry
    {
        public int countryID { set; get; }
        public string countryName { set; get; }



        private clsCountry(int countryID,string countryName)
        {
            this.countryID = countryID;
            this.countryName = countryName;

        }

       public static DataTable getCountryRecord()
        {
            DataTable dt = clsCountriesDataAccess.getCountryRecords();
            return dt;
        }

        public static clsCountry findCountry(int countryID)
        {
            string countryName = "";

      


            if (clsCountriesDataAccess.findCountry(countryID, ref countryName))
            {
                return new clsCountry(countryID, countryName);

            }
            return null;
        }

        public static bool isCountryExist(int countryID)
        {
            return clsCountriesDataAccess.isCountryExist(countryID);
        }
    }
}
