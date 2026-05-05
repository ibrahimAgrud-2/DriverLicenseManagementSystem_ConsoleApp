using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class clsLicenseCLass
    {

        public int licenseClassID { set; get; }
        public string licenseClassName { set; get; }

        public string classDescription { set; get; }
        public int minimumAge { set; get; }
        public int defaultValidityLength { set; get; }
        public double classFees { set; get; }






        public clsLicenseCLass()
        {
            this.licenseClassID = -1;
            this.licenseClassName = "";
            this.classDescription = "";
            this.minimumAge = -1;
            this.defaultValidityLength = -1;
            this.classFees = 0.0;
        }

        private clsLicenseCLass(int licenseClassID, string licenseClassName, string classDescription,
                                int minimumAge, int defaultValidityLength, double classFees)
        {
            this.licenseClassID = licenseClassID;
            this.licenseClassName = licenseClassName;
            this.classDescription = classDescription;
            this.minimumAge = minimumAge;
            this.defaultValidityLength = defaultValidityLength;
            this.classFees = classFees;
        }

      
        public static DataTable getLicenseClassRecords()
        {
            DataTable dt = new DataTable();

            dt = clsLicenseClassDataAccess.getLicenseClassesRecords();
            return dt;
        }



        public static clsLicenseCLass findLicenseClass(int licenseClassID)
        {
            string className = "";
            string classDescription = "";
            int minimumAge = -1;
            int defaultValidityLength = -1;
            double classFees = 0.0;

            if (clsLicenseClassDataAccess.findLicenseClass(licenseClassID, ref className, ref classDescription,
                                                           ref minimumAge, ref defaultValidityLength, ref classFees))
            {
                    return new clsLicenseCLass(licenseClassID, className, classDescription,
                                           minimumAge, defaultValidityLength, classFees);
            }

            return null;
        }



        public static bool isLicenseClassExist(int licenseClassID)
        {
            return clsLicenseClassDataAccess.isLicenseClassExist(licenseClassID);
        }


    }
}
