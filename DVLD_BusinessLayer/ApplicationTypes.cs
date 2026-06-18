using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ApplicationTypes
    {
        public int applicationTypeID { set; get; }
        public string applicantTypeTitle { set; get; }

        public double applicationFee { set; get; }



        public ApplicationTypes()
        {
            this.applicationTypeID = -1;
            this.applicantTypeTitle = "";
            this.applicationFee = 0.0;

        }

        private ApplicationTypes(int applicationTypeID, string applicantTypeTitle, double paidFees)
        {

            this.applicationTypeID = applicationTypeID;
            this.applicantTypeTitle = applicantTypeTitle;
            this.applicationFee = paidFees;
          

        }
        public static DataTable getApplicationTypeRecords()
        {
            DataTable dt = new DataTable();

            dt = ApplicationTypesDataAccess.getApplicationTypesRecords();
            return dt;
        }



        public static ApplicationTypes findApplicationType(int applicationID)
        {

            int applicationTypeID = -1;
            string applicantTypeTitle = "";
            double paidFees = 0.0;


            if (ApplicationTypesDataAccess.findApplicationType(applicationID,ref applicantTypeTitle, ref paidFees))
             
                {
                return new ApplicationTypes(applicationTypeID, applicantTypeTitle, paidFees);
            }
            return null;
        }



        public static bool isApplicationTypeExist(int applicationTypeID)
        {
            return ApplicationTypesDataAccess.isApplicationTypeExistByID(applicationTypeID);
        }

     

    }
}
