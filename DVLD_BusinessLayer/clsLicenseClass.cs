using DVLD_DataAccessLayer;
using System;
using System.Data;


namespace DVLD_BusinessLayer
{
    public class clsLicenseClass
    {
       public int licenseClassID { set; get; }
        string className { set; get; }
        string classDescription { set; get; }
        int minimumAge { set; get; }
        int defaultValidityLength { set; get; }
        double classFee { set; get; }

       public clsLicenseClass()
        {
            this.licenseClassID = -1;
            this.className = "";
            this.classDescription = "";
            this.minimumAge = 0;
            this.defaultValidityLength = 0;
            this.classFee = 0.0;
        }


        public clsLicenseClass(int licenseClassID,  string className,  string classDescription,  int minimumAge,  int defaultValidityLength, double classFee)
        {
            this.licenseClassID = licenseClassID;
            this.className = className;
            this.classDescription = classDescription;
            this.minimumAge = minimumAge;
            this.defaultValidityLength = defaultValidityLength;
            this.classFee = classFee;
        }

        public static DataTable getAllClassLicenseRecords()
        {
            return clsLicenseClassesDataAccess.getLicenseClassesRecords();
        }

        public static clsLicenseClass findLicenseClass(int licenseClassID)
        {
            string className="", classDescription="";
            int minimumAge=1, defaultValidityLength=1;
            double classFee=0.0;

            if (clsLicenseClassesDataAccess.findLicenseClass(licenseClassID,ref className, ref classDescription, ref minimumAge, ref defaultValidityLength, ref classFee))
            {
                return new clsLicenseClass(licenseClassID,  className,  classDescription,  minimumAge,  defaultValidityLength,  classFee);

            }
            return null;
        }

        public static bool isCLicenseClassExist(int licenseClassID)
        {
            return clsLicenseClassesDataAccess.isLicenseClassExit(licenseClassID);
        }


    }
}
