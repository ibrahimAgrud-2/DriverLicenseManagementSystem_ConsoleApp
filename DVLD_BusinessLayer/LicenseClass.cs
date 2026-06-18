using DVLD_DataAccessLayer;
using System;
using System.Data;


namespace DVLD_BusinessLayer
{
    public class LicenseClass
    {
       public int licenseClassID { set; get; }
        string className { set; get; }
        string classDescription { set; get; }
        int minimumAge { set; get; }
        int defaultValidityLength { set; get; }
        double classFee { set; get; }

       public LicenseClass()
        {
            this.licenseClassID = -1;
            this.className = "";
            this.classDescription = "";
            this.minimumAge = 0;
            this.defaultValidityLength = 0;
            this.classFee = 0.0;
        }


        public LicenseClass(int licenseClassID,  string className,  string classDescription,  int minimumAge,  int defaultValidityLength, double classFee)
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
            return LicenseClassesDataAccess.getLicenseClassesRecords();
        }

        public static LicenseClass findLicenseClass(int licenseClassID)
        {
            string className="", classDescription="";
            int minimumAge=1, defaultValidityLength=1;
            double classFee=0.0;

            if (LicenseClassesDataAccess.findLicenseClass(licenseClassID,ref className, ref classDescription, ref minimumAge, ref defaultValidityLength, ref classFee))
            {
                return new LicenseClass(licenseClassID,  className,  classDescription,  minimumAge,  defaultValidityLength,  classFee);

            }
            return null;
        }

        public static bool isCLicenseClassExist(int licenseClassID)
        {
            return LicenseClassesDataAccess.isLicenseClassExit(licenseClassID);
        }


    }
}
