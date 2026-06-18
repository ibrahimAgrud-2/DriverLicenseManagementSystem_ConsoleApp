using DVLD_DataAccessLayer;
using System;
using System.Data;


namespace DVLD_BusinessLayer
{
    public class Driver
    {
        public int driverID { set; get; }
        public int personID { set; get; }
        private int createdByUserID { set; get; }
        private DateTime createdDate { set; get; }

        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;

        public Driver()
        {
            this.driverID = -1;
            this.personID = -1;
            this.createdByUserID = -1;
            this.createdDate = DateTime.MinValue; // DateTime için default değer
            this.mode = enMode.enAddNew;
        }

        private Driver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.driverID = driverID;
            this.personID = personID;
            this.createdByUserID = createdByUserID;
            this.createdDate = createdDate;
            this.mode = enMode.enUpdate;
        }

        public static DataTable getDriverRecords()
        {
            DataTable dt = new DataTable();

            dt = clsDriverDataAccess.getDriverRecords();
            return dt;
        }



        public static Driver findDriver(int driverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            if (clsDriverDataAccess.findDriver(driverID, ref personID, ref createdByUserID, ref createdDate))
            {
                return new Driver(driverID, personID, createdByUserID, createdDate);
            }
            return null;
        }


        private bool _addNewDriver()
        {
            //bu değişecek. Bu o anki user kimse onun ID'sini alacak. Yani elle girilme olmayacak.

           
            this.createdDate = DateTime.Now;
            this.createdByUserID = 1;
            this.driverID = clsDriverDataAccess.addDriver(this.personID, this.createdByUserID,this.createdDate);
            return (this.driverID != -1);

        }
        private bool _updateDriverInfo()
        {

            return clsDriverDataAccess.updateDriverInfo(this.driverID, this.personID, this.createdByUserID, this.createdDate);
        }


        public static bool isDriverExistByDriverID(int Driver)
        {
            return clsDriverDataAccess.isDriverExistByDriverID(Driver);
        }
        public static bool isDriverExistByPersonID(int personID)
        {
            return clsDriverDataAccess.isDriverExistByPersonID(personID);
        }

        public static bool deleteDriver(int DriverID)
        {
            if (isDriverExistByDriverID(DriverID))
            {
                return clsDriverDataAccess.deleteDriver(DriverID);
            }
            return false;

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewDriver();

                case enMode.enUpdate:
                    return _updateDriverInfo();
                default:
                    return false;
            }
        }
    
    }

}
