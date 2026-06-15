using DVLD_DataAccessLayer;
using System;
using System.Data;


namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApp
    {

        public int id { set; get; }
        public int applicationID { set; get; }
        private int licenseClassID { set; get; }
     
        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;

        public clsLocalDrivingLicenseApp()
        {
            this.id = -1;
            this.applicationID = -1;
            this.licenseClassID = -1;
            this.mode = enMode.enAddNew;
        }

        private clsLocalDrivingLicenseApp(int id, int applicationID, int licenseClassID)
        {
            this.id = id;
            this.applicationID = applicationID;
            this.licenseClassID = licenseClassID;
            this.mode = enMode.enUpdate;
        }

        public static DataTable getLocalDrivingLicenseAppRecords()
        {
            DataTable dt = new DataTable();

            dt = clsLocalDrivingLicenseApp.getLocalDrivingLicenseAppRecords();
            return dt;
        }



        public static cls findLocalDrivingLicenseApp(int id)
        {
              int applicationID=0,
              licenseClassID=0;

            if (clsLocalDrivingLicenseApp.findLocalDrivingLicenseApp(id, ref applicationID, ref licenseClassID))
            {
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
            }
            return null;
        }


        private bool _addNewDriver()
        {
            //bu değişecek. Bu o anki user kimse onun ID'sini alacak. Yani elle girilme olmayacak.


            this.createdDate = DateTime.Now;
            this.createdByUserID = 1;
            this.driverID = clsDriverDataAccess.addDriver(this.personID, this.createdByUserID, this.createdDate);
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
