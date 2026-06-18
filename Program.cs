using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Xml;
using static DVLD_BusinessLayer.Licenses;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDConsoleAPP
{
    internal class Program
    {
        static void printAllPeopleRecords()
        {
            DataTable dt = new DataTable();

            dt = People.getAllPersonRecords();

            foreach (DataRow row in dt.Rows)
            { 
                Console.WriteLine($"{row["PersonID"]}, {row["NationalNo"]}, {row["FirstName"]}, {row["SecondName"]}, {row["ThirdName"]}, {row["LastName"]},  {row["DateOfBirth"]}, {row["Gender"]}, {row["Address"]}, {row["Phone"]}, {row["Email"]}, {row["NationalityCountryID"]}, :{row["ImagePath"]}");
            }
        
        }

        static void addPerson(string nationalNo,string firstName,string secondName,string thirdName,string lastName,DateTime dateOfBirth,int gender,string address,string phone,string email,int countryID,string imagePath)
        {

            People p1 = new People();
            if (People.isPersonExistByNationalNo(nationalNo))
            {
                Console.WriteLine("person with "+ nationalNo + " national number is already exist.");
                return;
            }
            p1.nationalNo = nationalNo;
            p1.firstName = firstName;
            p1.secondName = secondName;
            p1.thirdName = thirdName;
            p1.lastName = lastName;
            p1.dateOfBirth = dateOfBirth;
            p1.email = email;
            p1.phone = phone;
            p1.address = address;
            p1.gender = gender;
            p1.countryID = countryID;
            p1.imagePath = imagePath;

            if (p1.save())
            {
                Console.WriteLine("\n\nSaved with ID "+p1.personID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        static void updatePersonInfo(int personID)
        {
            
            if (!People.isPersonExistByID(personID))
            {
                Console.WriteLine("Person Does not exist");
                return;
            }
            People p1 = People.findPersonByID(personID);
            Console.WriteLine("enter birthdate");
            p1.dateOfBirth =Convert.ToDateTime(Console.ReadLine());

            if (p1.save())
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
      
        static void deletePerson(int personID)
        {
            if (!People.isPersonExistByID(personID))
            {
                Console.WriteLine("person does not exist");
                return;
            }

            if (People.delete(personID))
            {
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        
        }
     
        //========================User================================

        static void printUsers()
        {
            DataTable dt = new DataTable();

            dt = User.getUserRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["licenseID"]},{row["PersonID"]}, {row["userName"]}, {row["password"]}, {row["isActive"]}");
            }
        }
        
        static void addUser(int personID,string userName,string password,bool isActive)
        {

            User App1 = new User();
            if (!People.isPersonExistByID(personID))
            {
                Console.WriteLine("Person with ID {0} could not found!", personID);
                return;
            }
            App1.personID = personID;
            App1.userName = userName;
            App1.password = password;
            App1.isActive = isActive;
           
            if (App1.save())
            {
                Console.WriteLine("\n\nSaved with ID " + App1.userID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
      
        static void deleteUser(int userID)
        {
            if (!User.isUserExist(userID))
            {
                Console.WriteLine("user does not exist");
                return;
            }

            if (User.deleteUser(userID))
            {
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }
   
        static void updateUser(int userID)
        {

            if (!User.isUserExist(userID))
            {
                Console.WriteLine("licenseID Does not exist");
                return;
            }
            User App1 = User.findUser(userID);
            Console.WriteLine("enter user name");
            App1.userName = Console.ReadLine();


            if (App1.save())
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        //========================Countries================================

        static void printCountries()
        {
            DataTable dt = new DataTable();

            dt = Country.getCountryRecord();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["countryID"]},{row["countryName"]}");
            }
        }
        //=======================Applications=======================

        static void printApplications()
        {
            DataTable dt = new DataTable();

            dt = Applications.getApplicationsRecord();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["licenseID"]},{row["applicantPersonID"]}, {row["applicationDate"]}, {row["applicationTypeID"]}, {row["applicationStatus"]}, {row["lastStatusDate"]}, {row["PaidFees"]}, {row["createdByUserID"]}");
            }
        }

        static void addApplication(int applicantPersonID, int applicationTypeID, byte appStatus)
        {
           
            if (!People.isPersonExistByID(applicantPersonID))
            {
                Console.WriteLine("Person with ID {0} could not found!", applicantPersonID);
                return;
            }
            else if (!ApplicationTypes.isApplicationTypeExist(applicationTypeID))
            {
                Console.WriteLine("AppType with ID {0} could not found!", applicationTypeID);
                return;
            }
            Applications App1 = new Applications();
            App1.applicantPersonID = applicantPersonID;
            App1.applicationTypeID = applicationTypeID;
            App1.applicationStatus = (Applications.enApplicationStatus)appStatus;

            if (App1.save())
            {
                Console.WriteLine("\n\nSaved with ID " + App1.applicationID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        static void updateApplication(int applicationID)
        {

          
           if (!Applications.isApplicationExist(applicationID))
            {
                Console.WriteLine("App with ID {0} could not found!", applicationID);
                return;
            }

            Applications App1 = Applications.findApplication(applicationID);
            Console.WriteLine("Enter new person ID ");
            App1.applicantPersonID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter App Type");
            App1.applicationTypeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter App status");
            App1.applicationStatus = (Applications.enApplicationStatus)(Convert.ToByte(Console.ReadLine()));




            if (App1.save())
            {
                Console.WriteLine("\nupdated ");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
        //=======================Application Types======================
        static void printApplicationTypes()
        {
            DataTable dt = new DataTable();

            dt = ApplicationTypes.getApplicationTypeRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["applicationTypeID"]},{row["applicationTypeTitle"]},{row["applicationFees"]}");
            }
        }

        //=======================LicenseCLasses======================
        static void printLicenseClasses()
        {
            DataTable dt = new DataTable();

            dt = Licenses.getLicenseRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["LicenseClassID"]},{row["ClassName"]},{row["ClassDescription"]},{row["MinimumAge"]},{row["DefaultValidityLength"]},{row["ClassFees"]}");
            }
        }

        //=======================Driver======================
        static void printDrivers()
        {
            DataTable dt = new DataTable();

            dt = Driver.getDriverRecords(); // Driver kayıtlarını getiren metod

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["DriverID"]},{row["PersonID"]},{row["CreatedByUserID"]},{row["CreatedDate"]}");
            }
        }
        static void addDriver(int personID)
        {

          
            if (!People.isPersonExistByID(personID))
            {
                Console.WriteLine("Person with ID {0} could not found!", personID);
                return;
            }

            Driver driver1 = new Driver();
            driver1.personID = personID;
            if (driver1.save())
            {
                Console.WriteLine("Saved successfully with ID {0}", driver1.driverID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        static void deleteDriver(int driverID)
        {
            if (!Driver.isDriverExistByDriverID(driverID))
            {
                Console.WriteLine("driver does not exist");
                return;
            }

            if (Driver.deleteDriver(driverID))
            {
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }
        //=======================License======================

        static void printLicenses()
        {
            DataTable dt = new DataTable();

            dt = Licenses.getLicenseRecords(); // Driver kayıtlarını getiren metod

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["LicenseID"]},{row["ApplicationID"]},{row["DriverID"]},{row["LicenseClass"]},{row["IssueDate"]},{row["ExpirationDate"]},{row["Notes"]},{row["PaidFees"]},{row["IsActive"]},{row["IssueReason"]},{row["CreatedByUserID"]}");
            }
        }
        static void addLicense(int applicationID, int DriverID, int licenseClassID, DateTime expirationDate,string notes,bool isActive,int issueReason)
        {


            if (!Applications.isApplicationExist(applicationID))
            {
                Console.WriteLine("Application with ID {0} could not found!", applicationID);
                return;
            }
            if (!Driver.isDriverExistByDriverID(DriverID))
            {
                Console.WriteLine("DriverID with ID {0} could not found!", DriverID);
                return;
            }
            if (!Licenses.isLicenseExist(licenseClassID))
            {
                Console.WriteLine("license Class ID with ID {0} could not found!", licenseClassID);
                return;
            }


            Licenses l1 = new Licenses();
            l1.applicationID = applicationID;
            l1.driverID = DriverID;
            l1.licenseClass = licenseClassID;
            l1.expirationDate = expirationDate;
            l1.notes = notes;
            l1.isActive = isActive;
            l1.issueReason = (Licenses.enIssueReason)issueReason;
            if (l1.save())
            {
                Console.WriteLine("Saved successfully with ID {0}", l1.licenseID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        static void updateLicense(int licenseID)
        {


            if (!Licenses.isLicenseExist(licenseID))
            {
                Console.WriteLine("license with ID {0} could not found!", licenseID);
                return;
            }

            Licenses l1 = Licenses.findLicense(licenseID);
            if (l1==null)
            {
                Console.WriteLine("Yes");
                return;
            }

            Console.WriteLine("Enter new App ID ");
            l1.applicationID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter driver ID");
            l1.driverID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter License Class ID ");
            l1.licenseClass = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Expiration Date");
            l1.expirationDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Noets ");
            l1.notes = Console.ReadLine().ToString();
            Console.WriteLine("Is Active");
            l1.isActive = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Issue reason");
            l1.issueReason = (Licenses.enIssueReason)Convert.ToInt32(Console.ReadLine());






            if (l1.save())
            {
                Console.WriteLine("\nupdated ");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
        static void deleteLicense(int licenseID)
        {
            if (!Licenses.isLicenseExist(licenseID))
            {
                Console.WriteLine("License  does not exist");
                return;
            }

            if (Licenses.deleteLicense(licenseID))
            {
                Console.WriteLine("Deleted successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }

        //=======================DetainedLicense======================

        static void addDetainedLicense(int licenseID,double fineFee,int releasedAppID)
        {
           
            if (!Licenses.isLicenseExist(licenseID))
            {
                Console.WriteLine("licenseID with ID {0} could not found!", licenseID);
                return;
            }
         


            DetainedLicense dl1 = new DetainedLicense();
            dl1.licenseID = licenseID;
            dl1.fineFees = fineFee;
            dl1.releaseApplicationID  = releasedAppID;
   
            if (dl1.save())
            {
                Console.WriteLine("Saved successfully with ID {0}", dl1.licenseID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }

        static void updateDetainedLicense(int detainID)
        {


            DetainedLicense dl1 = DetainedLicense.findDetainedLicense(detainID);
            dl1.licenseID = 27;
            dl1.isReleased = false;
            dl1.releaseApplicationID = 130;

           

            if (dl1.save())
            {
                Console.WriteLine("\nupdated ");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
        //=======================DetainedLicense======================

      
         static void Main(string[] args)
        {

            //Gelişim Sorularu
            /*
             - mesela database'de firstName null değer kabul etmesin. veriyi database'e
            gönderirken null kontolu hangi katmanda yapmalıyız? Aynı şey başkta tablodaki verini olup olmadığını kontrl ederken de geçerli. Mesela,detained license eklerken eklenen licenseID gerçekten tabloda var mı? Şu an en mantıklı gelen BL'de çünkü eğer PL'de yazarsam; ilgil kontrol kodunu her platform için (web, mobil vs) ayrı yazmam  gerekir.
          

             **Eğer dataAccess layer'da yapmazsak bu dll'i başka yerde kullandığımızda aynı kontrolleri o arayüzde de yapmamız
            *gerekir yani bu kısım arayüzde bağımlı olur** Meselae bunu önüne şu şelilde geçebiliriz. sadece parametereli
            *const'u public yaparız. BU sayede kullanıc obje oluştutmak için verileri girmek zorunda. Zaten save fonskyionu 
            *objeye bağlı olduğu için anca obje ile çağırılabili. Yani save yapacapımız zaman o objenin tüm verileri düzenli
            *bir şekilde parametreli const tarafından alınmış olur.


            - People sınıfın için Constracter yapısında bir değişiklik var mı? ben birini private birini public yaptım hoca nasıl yapmış?
            - mesela şu an driver eklerken peopleID'de kısmınsa sorun oluıyor. sistemde eğer bir kişi driver'sa o başka tekrara direver olarak eklenemez. Yani sistem bir driver eklerken 1) ilk olarak o Kişi var 2)o kişi zaten driver mı diye kontrol etmeliyiz. Bu kontrol hangi katmanda yapmamız en mantıkı olur? Ben şahsen bunu UI ile sınırlı olmasını istemiyorum. Bir yandan da ya zaten DB böyle bir şeyi eklemeye izin vermez hata olur ve ekleme başarısız olur der bu nedenle tıpkı user eklerken people var mı diye kontolu UI'da yapmıştık, bunuda UI'da yani burda yapalım diyorum.
                     //Read only olan kısımlar var. Mesela application eklerken licenseID elle girilmesin. bunu sistem o anki hangi kullanıcı aktifse onun ID'sini eklemeli. Bunu gibi readOnly olan durumlar var. mesela app eklerken ödene tutarı sistem direk appTypes'tan getirsin otomatik

     
            //APP laststatus time ne zaman güncellenmeli. Acaba bu UI olduğunda daha mı kolay olur? mesela console ile bunu nasıl yapacağız. UI ile kullanıcı kutularda istrediği kısmı günceller sonra DB'e kayıt ederken eğer status değişmişse anca o zaman status'u değiştiriri.

            //Driver silecekken hata alıyorum çünkü bazı driver'lar refereans var. Yani 8 ID'li driver license yani licenseID'nin FK olarak ekli olduğu tabloda kayıtlı olduğu için 8 ID'Li driver silinmiyor
             */
            //Sİstemde kullanıcını girmemesi gereken kısımlar var mesela createdByUserID, bunu hoca girilmesine/değiştirilmesine izin vermişmi.



            //SIK COMMİT (Güzel görünsün hem adımlar detaylu olsun)ü


            //Acaba ilişki tablosundan sadece ID tutmak yerine o sınıfı da tutmalımıyız. Hoca böyle yapmış mıdır? SInıfın içinde property olarak diğer sınıf olacak ve veri eklerken her bri record için ilişkiye göre diğer kısımlarda eklenecek bu sayede erişimm kolaylaşı

            Tests t1 = new Tests();


            t1.notes = "Bad";
            t1.testAppointmentID = 108;
            t1.testResult = 0;



            Console.WriteLine(t1.save());

        }
    }   
}



