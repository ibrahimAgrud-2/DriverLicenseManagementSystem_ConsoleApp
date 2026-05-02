using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;


namespace DVLD_BusinessLayer
{
    public class clsApplications
    {
        public int applicationID { set; get; }
        public int applicantPersonID { set; get; }
        public int applicationTypeID { set; get; }
        //İkiside private yaptım çünkü; son güncelleme tarihi sistem tarafından kara verilebilir. SOnradan değiştirilebilen veya
        //kullanıcı tarafından yanlış girilebilen bir şey olmamalı
        private DateTime lastStatusDate{ set; get; }
        public DateTime applicationDate { set; get; }
        //private yaptım çünkü ödenen değer elle girilmemeli. Sistem başvuru türüne göre o başvuru için gerekli olan ücreti yazmalı
        private double paidFee { set; get; }
        //Yukaridaki benzer sebeplerden ötürü private olmalı.
        private int createdByUserID { set; get; }


        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enum enApplicationStatus {New=1, Cancelled=2 ,Completed =3};
       public enApplicationStatus applicationStatus;

        public enMode mode;


        public clsApplications()
        {
            this.applicationID = -1;
            this.applicantPersonID = -1;
            this.createdByUserID = -1;
            this.applicationDate=DateTime.Now;
            this.applicationTypeID = -1;
            this.applicationStatus = enApplicationStatus.New;
            this.lastStatusDate = DateTime.Now;
            this.paidFee = 0.0;
            this.mode=enMode.enAddNew;
        }

        private clsApplications(int applicationID, int applicantPersonID, DateTime ApplicationDate, int applicationTypeID,
           enApplicationStatus applicationStatus, DateTime LastStatusDate, double paidFee, int createdByUserID)
        {

            this.applicationID = applicationID;
            this.applicantPersonID = applicantPersonID;
            this.applicationDate = ApplicationDate;
            this.applicationTypeID = applicationTypeID;
            this.applicationStatus = applicationStatus;
            this.lastStatusDate = LastStatusDate;
            this.paidFee = paidFee;
            this.createdByUserID = createdByUserID;
            this.mode = enMode.enUpdate;

        }
        public static DataTable getApplicationsRecord()
        {
            DataTable dt = new DataTable();

            dt = clsApplicationsDataAccess.getApplicationsRecord();
            return dt;
        }



        public static clsApplications findApplication(int applicationID)
        {

            int applicantPersonID = -1, createdByUserID=-1, applicationTypeID=-1;
            DateTime applicationDate = DateTime.Now, lastStatusDate=DateTime.Now;
            byte applicationStatus = 0;
            double paidFee = 0.0;



            if (clsApplicationsDataAccess.findApplication( applicationID, ref  applicantPersonID, ref  applicationDate, ref  applicationTypeID, ref
            applicationStatus, ref lastStatusDate, ref  paidFee, ref  createdByUserID))
            {
                return new clsApplications(applicationID,  applicantPersonID,  applicationDate,  applicationTypeID, 
            (enApplicationStatus)applicationStatus,  lastStatusDate,  paidFee,  createdByUserID);

            }
            return null;
        }


        private bool _addNewApplication()
        {

            //Başvuru ücretini elle girmemek için bu kodu ekledirk.
            clsApplicationTypes type1 = clsApplicationTypes.findApplicationType(this.applicationTypeID);
            if (type1 == null)
            {
                return false;
            }
            this.paidFee = type1.applicationFee;

            //Normalde bu bilgi o anki giriş yapan kullanıcı bilgilerinde çekilir ama şu anda giriş ekranı daha yok. 
            //Giriş ekranı olduğunda kullanıcı aktif kullanıcı bilgilerinden çekilir.
            this.createdByUserID = 1;
            this.applicationID = clsApplicationsDataAccess.addApplication(this.applicantPersonID, this.applicationTypeID, 
                Convert.ToByte(this.applicationStatus), this.lastStatusDate, this.paidFee, this.createdByUserID);
            return (this.applicationID != -1);

        }

        //Update yaparken lastStatus güncellenmeli.
        private bool _updateApplication()
        {
            this.lastStatusDate = DateTime.Now;
            return clsApplicationsDataAccess.updateApplicationInfo(this.applicationID,this.applicantPersonID, this.applicationDate, this.applicationTypeID, Convert.ToByte(this.applicationStatus), this.lastStatusDate, this.paidFee, this.createdByUserID);
        }


        public static bool isApplicationExist(int applicationID)
        {
            return clsApplicationsDataAccess.isApplicationExistByID(applicationID);
        }

        public static bool deleteApplication(int applicationID)
        {
            if (isApplicationExist(applicationID))
            {
                return clsApplicationsDataAccess.deleteApplication(applicationID);
            }
            return false;

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewApplication();

                case enMode.enUpdate:
                    return _updateApplication();
                default:
                    return false;
            }
        }


    }
}
