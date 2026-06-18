using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class Licenses
    {
        public int licenseID { set; get; }
        public int applicationID { set; get; }
        public int driverID { set; get; }
        public int licenseClass { set; get; }

   
        public DateTime issueDate { set; get; }
        public DateTime expirationDate { set; get; }

        public string notes { set; get; }

        
        private double paidFees { set; get; }

        public bool isActive { set; get; }

        private int createdByUserID { set; get; }

        public double PaidFees { set; get; }

        public enum enMode { enAddNew = 1, enUpdate = 2 }; 
        public enMode mode;
        public enum enIssueReason {FirstTime=1, Renew=2, ReplacementForDamaged =3, ReplacementForLost = 4 };
        public enIssueReason issueReason;




        public Licenses()
        {
            this.licenseID = -1;
            this.applicationID = -1;
            this.driverID = -1;
            this.licenseClass = -1;
            this.issueDate = DateTime.Now;
            this.expirationDate = DateTime.Now.AddYears(5);
            this.notes = null;
            this.paidFees = 0.0;
            this.isActive = true;
            this.createdByUserID = -1;
            this.PaidFees = 0.0;
            this.issueReason = enIssueReason.FirstTime;
            this.mode = enMode.enAddNew;
        }

        private Licenses(int licenseID, int applicationID, int driverID, int licenseClass,
            DateTime issueDate, DateTime expirationDate, string notes, double paidFees,
            bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            this.licenseID = licenseID;
            this.applicationID = applicationID;
            this.driverID = driverID;
            this.licenseClass = licenseClass;
            this.issueDate = issueDate;
            this.expirationDate = expirationDate;
            this.notes = notes;
            this.paidFees = paidFees;
            this.isActive = isActive;
            this.issueReason = issueReason;
            this.createdByUserID = createdByUserID;
            this.PaidFees = paidFees;
            this.issueReason = issueReason;
            this.mode = enMode.enUpdate;
        }
        public static DataTable getLicenseRecords()
        {
            DataTable dt = new DataTable();

            dt = LicensesDataAccess.getLicenseRecords();
            return dt;
        }



        public static Licenses findLicense(int licenseID)
        {
            int applicationID = -1, driverID = -1, licenseClass = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = "";
            double paidFees = 0;
            bool isActive = false;
            int issueReasonInt = 1;

            if (LicensesDataAccess.findLicense(licenseID, ref applicationID, ref driverID, ref licenseClass,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReasonInt, ref createdByUserID))
            {
                return new Licenses(licenseID, applicationID, driverID, licenseClass,
                    issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason)issueReasonInt, createdByUserID);
            }

            return null;
        }


        private bool _addNewLicense()
        {

            //Yeni veri eklerekn,bazı kısımların kontrol edilmesi lazım
            //User'in girmemesi gerek şeyleri buradan biz sistemden alıyoruz. mesela userID, paidFee for application gibi

            //şu anlık 1 ama login screen olduğunda bu değişiek.
            this.createdByUserID = 1;
            this.paidFees = 10;
            this.issueDate = DateTime.Now;

            this.licenseID = LicensesDataAccess.addLicense(this.applicationID,this.driverID,this.licenseClass,this.issueDate,this.expirationDate,this.notes,this.paidFees,this.isActive,Convert.ToInt32(this.issueReason),this.createdByUserID);
            return (this.applicationID != -1);

        }

        private bool _updateLicense()
        {

            return LicensesDataAccess.updateLicenseInfo(this.licenseID,this.driverID,this.licenseClass,this.expirationDate,this.notes    ,this.paidFees,this.isActive,Convert.ToInt32( this.issueReason));
        }


        public static bool isLicenseExist(int licenseID )
        {
            return LicensesDataAccess.isLicenseExist(licenseID);
        }

        public static bool deleteLicense(int licenseID)
        {
            if (isLicenseExist(licenseID))
            {
                return LicensesDataAccess.deleteLicense(licenseID);
            }
            return false;

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewLicense();

                case enMode.enUpdate:
                    return _updateLicense();
                default:
                    return false;
            }
        }

    }
}
