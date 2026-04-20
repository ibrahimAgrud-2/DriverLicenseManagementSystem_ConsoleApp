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
        public int createdByUserID { set; get; }
        public DateTime applicationDate { set; get; }
        public int applicationTypeID { set; get; }
        public DateTime lastStatusDate{ set; get; }
        public double paidFee { set; get; }


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
            this.applicationID = clsApplicationsDataAccess.addApplication(this.applicantPersonID, this.applicationDate, this.applicationTypeID, Convert.ToByte(this.applicationStatus), this.lastStatusDate, this.paidFee, this.createdByUserID);
            return (this.applicationID != -1);

        }
        private bool _updateApplicatİonInfo()
        {

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
                return clsApplicationsDataAccess.deletePerson(applicationID);
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
                    return _updateApplicatİonInfo();
                default:
                    return false;
            }
        }


    }
}
