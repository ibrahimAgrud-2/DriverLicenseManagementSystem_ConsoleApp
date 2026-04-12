using DVLD_DataAccessLayer;
using System;
using System.Data;



namespace DVLD_BusinessLayer
{
    public class clsPeople
    {
  
        public int ID { set; get; }
        public string nationalNo { set; get; }
        public string firstName { set; get; }
        public string secondName { set; get; }
        public string thirdName { set; get; }
        public string lastName { set; get; }
        public DateTime dateOfBirth { set; get; }
        public string gender { set; get; }
        public string address { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public int countryID { set; get; }
        public string imagePath { set; get; }

        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;



    
    }
}
