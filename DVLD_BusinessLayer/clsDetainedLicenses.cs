using System;
using System.Data;
using DVLD_DataAccessLayer;


namespace DVLD_BusinessLayer
{
    public class clsDetainedLicenses
    {
        int ID { set; get; }
        int licenseID { set; get; }
        DateTime DetainDate { set; get; }
        double fineFees { set; get; }
        int CreatedByUserID { get; set; }
        bool isReleased { set; get; }
        DateTime releaseDate { set; get; }
        int ReleasedByUserID { set; get; }
        int releaseApplicationID { set; get; }

        public enum enMode {enAddNew=1,enUpdate=2};
        public enMode mode;


        public clsDetainedLicenses()
        {
            this.ID = -1;
            this.licenseID = -1;
            this.DetainDate = DateTime.Now;
            this.fineFees =0.0;
            this.CreatedByUserID =- 1;
            this.isReleased = false;
            this.releaseDate =DateTime.Now;
            this.ReleasedByUserID = -1;
            this.releaseApplicationID = -1;
        }

        public clsDetainedLicenses(int ID,int licenseID,DateTime detainDate,double fineFee,int createdByUserID,bool isReleased,DateTime releaseDate,int releasedByUserID,int releasedApplicationID)
        {
            this.ID = ID;
            this.licenseID = licenseID;
            this.DetainDate = detainDate;
            this.fineFees = fineFee;
            this.CreatedByUserID = createdByUserID;
            this.isReleased = isReleased;
            this.releaseDate = releaseDate;
            this.ReleasedByUserID = releasedByUserID;
            this.releaseApplicationID = releasedApplicationID;
        }
    
        public DataTable getAllDetainedLicenseRecords()
        {
            DataTable dt = new DataTable();
            dt=clsDetainedLicensesDataAccess.getDetainedLicenseRecords();


            return dt;
        }

    }
}
