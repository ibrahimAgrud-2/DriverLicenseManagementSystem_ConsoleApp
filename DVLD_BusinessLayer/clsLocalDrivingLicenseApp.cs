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



        public static clsLocalDrivingLicenseApp findLocalDrivingLicenseApp(int id)
        {
              int applicationID=0,
              licenseClassID=0;

            if (clsLocalDrivingLicenseAppDataAccess.findLocalDrivingLicenseApp(id, ref applicationID, ref licenseClassID))
            {
                return new clsLocalDrivingLicenseApp(id, applicationID, licenseClassID);
            }
            return null;
        }


        private bool _addNewLocalDriverLicenseApp()
        {


            this.licenseClassID = 1;
            this.applicationID = 1;
            this.id = clsLocalDrivingLicenseAppDataAccess.addLocalDrivingLicense( this.licenseClassID, this.applicationID);
            return (this.id != -1);

        }
        private bool _updateDLocalDriverLicenseAppInfo()
        {

            return clsLocalDrivingLicenseAppDataAccess.updateLocalDrivingLicenseInfo(this.id, this.applicationID,this.licenseClassID);
        }


  
        public static bool isLocalDriverLicenseExist(int id)
        {
            return clsLocalDrivingLicenseApp.isLocalDriverLicenseExist(id);
        }

        public static bool deleteDriver(int DriverID)
        {
       
            return clsLocalDrivingLicenseAppDataAccess.deleteLocalDrivingLicenseApp(DriverID);

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewLocalDriverLicenseApp();

                case enMode.enUpdate:
                    return _updateDLocalDriverLicenseAppInfo();
                default:
                    return false;
            }
        }
    }
}
