using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Diagnostics;


namespace DVLD_BusinessLayer
{
    public class DetainedLicense
    {
         int detainID { set; get; }
       public int licenseID { set; get; }

        //detain tarihi sitem tarafından belirlenir (otomatik)
            private  DateTime _detainDate { set; get; }
           public   double fineFees { set; get; }
        //Sİsmte belirler. İşlemi yapan kullanıcı kimse.
        private int _createdByUserID { set; get; }
           public  bool isReleased { set; get; }
            DateTime releaseDate { set; get; }
        //Sİsmte belirler. İşlemi yapan kullanıcı kimse.
        private int _releasedByUserID { set; get; }
          public  int releaseApplicationID { set; get; }

        enum enMode { enAddNew=1,enUpdate=2};
        enMode mode;


      public  DetainedLicense()
        {
            this.licenseID = -1;
            this.licenseID = -1;
            this._detainDate = DateTime.Now;
            this.fineFees = 0.0;
            this._createdByUserID = -1;
            this.isReleased = false;
            this.releaseDate = DateTime.Now;
            this._releasedByUserID = -1;
            this.releaseApplicationID = -1;
            this.mode = enMode.enAddNew;
        }
       private DetainedLicense(int detainID, int licensedID,  DateTime detainDate,  double fineFees,  int createdByUserID,  bool isReleased,  DateTime releaseDate,  int releasedByUserID,  int releaseApplicationID)
        {
            this.detainID = detainID;
            this.licenseID = licensedID;
            this._detainDate = detainDate;
            this.fineFees = fineFees;
            this._createdByUserID = createdByUserID;
            this.isReleased = isReleased;
            this.releaseDate = releaseDate;
            this._releasedByUserID = releasedByUserID;
            this.releaseApplicationID = releaseApplicationID;
            this.mode = enMode.enUpdate;
        }

        static DataTable getDetainedLicenseRecords()
        {
            return DetainedLicensesDataAccess.getDetainedLicenseRecords();
        }
          
       public static DetainedLicense findDetainedLicense(int detainID)
        {
          int licenseID = -1, releaseApplicationID=-1, releasedByUserID=-1, createdByUserID=-1;
              DateTime detainDate = DateTime.Now, releaseDate = DateTime.Now;
            double fineFees = 0.0;
            bool isReleased = false;

            if (DetainedLicensesDataAccess.findDetainedLicense(detainID, ref licenseID,ref detainDate, ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID))
            {
                return new DetainedLicense(detainID, licenseID, detainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            return null;



        }


        private bool _addNewDetainedLicense()
        {

            //bunlar şimdilik böyle ama gerekli ayarlamalar yapıldıktan sonra (arayüz, admin/user sistemi) sistem otomatik belirleyecek
            this._createdByUserID = 1;
            this._detainDate = DateTime.Now;
            this._releasedByUserID = 1;

       

            this.detainID = DetainedLicensesDataAccess.addDetainedLicense(this.licenseID, this._detainDate, this.fineFees, this._createdByUserID, this.isReleased, this.releaseDate, this._releasedByUserID, this.releaseApplicationID);


            return (this.detainID != -1);

        }

        private bool _updateDetainedLicense()
        {
            this._createdByUserID = 1;
            this._releasedByUserID = 1;
            this.fineFees = 12;
            this._detainDate = DateTime.Now;
            this.releaseDate = DateTime.Now;
            

            return DetainedLicensesDataAccess.updateDetainedLicense(this.detainID,this.licenseID, this._detainDate, this.fineFees, this._createdByUserID, this.isReleased, this.releaseDate, this._releasedByUserID, this.releaseApplicationID);
        }
         public  static bool isDetainedLicenseExist(int detainID)
        {
            return DetainedLicensesDataAccess.isDetainedLicenseExist(detainID);
        }

        public bool save()
        {
            switch(this.mode)
            {
                case enMode.enAddNew:
                    return _addNewDetainedLicense();
                case enMode.enUpdate:
                    return _updateDetainedLicense();
                default:
                    return false;
            }
        }
    }
}
