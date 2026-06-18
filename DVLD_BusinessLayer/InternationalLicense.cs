using DVLD_DataAccessLayer;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD_BusinessLayer
{
    public class InternationalLicense
    {

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;

        public InternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);
            this.IsActive = false;
            this.CreatedByUserID = -1;
            this.mode = enMode.enAddNew;
        }

        private InternationalLicense(int InternationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;
            this.mode = enMode.enUpdate;
        }

        public static DataTable getInternationalLicenseRecords()
        {
            DataTable dt = new DataTable();

            dt = InternationalLicenseDataAccess.getInternationalLicenseRecords();
            return dt;
        }



        public static InternationalLicense findInternationalLicenses(int InternationalLicenseID)
        {
           int issuedUsingLocalLicenseID = -1,  createdByUserID = -1, ApplicationID=-1, DriverID=-1;

           DateTime issueDate = DateTime.MinValue,  expirationDate = DateTime.MinValue;

            bool isActive = true;


            if (InternationalLicenseDataAccess.findInternationalLicenses(InternationalLicenseID,ref ApplicationID,ref DriverID, ref issuedUsingLocalLicenseID, ref issueDate, ref expirationDate, ref isActive, ref createdByUserID))
            {
                return new InternationalLicense(InternationalLicenseID,  ApplicationID,  DriverID,  issuedUsingLocalLicenseID,  issueDate,  expirationDate,  isActive,  createdByUserID);
            }
            return null;
        }


        private bool _addNewInternationalLicenseInfo()
        {
   

            this.CreatedByUserID = 1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
           


            this.InternationalLicenseID = InternationalLicenseDataAccess.addInternationalLicense(this.ApplicationID,this.DriverID,this.IssuedUsingLocalLicenseID,this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);

        }
        private bool _updateInternationalLicenseInfo()
        {

            return InternationalLicenseDataAccess.updateInternationalLicenseInfo(this.InternationalLicenseID,this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }


        public static bool isDriverExistInternationalLicense(int id)
        {
            return InternationalLicenseDataAccess.isInternationalLicenseExist(id);
        }
   

        public static bool deleteInternationalLicense(int id)
        {
            if (isDriverExistInternationalLicense(id))
            {
                return InternationalLicenseDataAccess.deleteInternationalLicense(id);
            }
            return false;

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewInternationalLicenseInfo();

                case enMode.enUpdate:
                    return _updateInternationalLicenseInfo();
                default:
                    return false;
            }
        }
    }
}
