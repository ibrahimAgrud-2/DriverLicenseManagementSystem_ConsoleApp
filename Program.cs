using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Xml;


namespace DVLDConsoleAPP
{
    internal class Program
    {
        static void printAllPeopleRecords()
        {
            DataTable dt = new DataTable();

            dt = clsPeople.getAllPersonRecords();

            foreach (DataRow row in dt.Rows)
            { 
                Console.WriteLine($"{row["PersonID"]}, {row["NationalNo"]}, {row["FirstName"]}, {row["SecondName"]}, {row["ThirdName"]}, {row["LastName"]},  {row["DateOfBirth"]}, {row["Gender"]}, {row["Address"]}, {row["Phone"]}, {row["Email"]}, {row["NationalityCountryID"]}, :{row["ImagePath"]}");
            }
        
        }

        static void addPerson(string nationalNo,string firstName,string secondName,string thirdName,string lastName,DateTime dateOfBirth,int gender,string address,string phone,string email,int countryID,string imagePath)
        {

            clsPeople p1 = new clsPeople();
            if (clsPeople.isPersonExistByNationalNo(nationalNo))
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
            
            if (!clsPeople.isPersonExistByID(personID))
            {
                Console.WriteLine("Person Does not exist");
                return;
            }
            clsPeople p1 = clsPeople.findPersonByID(personID);
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
            if (!clsPeople.isPersonExistByID(personID))
            {
                Console.WriteLine("person does not exist");
                return;
            }

            if (clsPeople.delete(personID))
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

            dt = clsUser.getUserRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["userID"]},{row["PersonID"]}, {row["userName"]}, {row["password"]}, {row["isActive"]}");
            }
        }
        
        static void addUser(int personID,string userName,string password,bool isActive)
        {

            clsUser App1 = new clsUser();
            if (!clsPeople.isPersonExistByID(personID))
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
            if (!clsUser.isUserExist(userID))
            {
                Console.WriteLine("user does not exist");
                return;
            }

            if (clsUser.deleteUser(userID))
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

            if (!clsUser.isUserExist(userID))
            {
                Console.WriteLine("userID Does not exist");
                return;
            }
            clsUser App1 = clsUser.findUser(userID);
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

            dt = clsCountry.getCountryRecord();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["countryID"]},{row["countryName"]}");
            }
        }
        //=======================Applications=======================

        static void printApplications()
        {
            DataTable dt = new DataTable();

            dt = clsApplications.getApplicationsRecord();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["applicationID"]},{row["applicantPersonID"]}, {row["applicationDate"]}, {row["applicationTypeID"]}, {row["applicationStatus"]}, {row["lastStatusDate"]}, {row["PaidFees"]}, {row["createdByUserID"]}");
            }
        }

        static void addApplication(int applicantPersonID, int applicationTypeID, byte appStatus)
        {
           
            if (!clsPeople.isPersonExistByID(applicantPersonID))
            {
                Console.WriteLine("Person with ID {0} could not found!", applicantPersonID);
                return;
            }
            else if (!clsApplicationTypes.isApplicationTypeExist(applicationTypeID))
            {
                Console.WriteLine("AppType with ID {0} could not found!", applicationTypeID);
                return;
            }
            clsApplications App1 = new clsApplications();
            App1.applicantPersonID = applicantPersonID;
            App1.applicationTypeID = applicationTypeID;
            App1.applicationStatus = (clsApplications.enApplicationStatus)appStatus;

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

          
           if (!clsApplications.isApplicationExist(applicationID))
            {
                Console.WriteLine("App with ID {0} could not found!", applicationID);
                return;
            }

            clsApplications App1 = clsApplications.findApplication(applicationID);
            Console.WriteLine("Enter new person ID ");
            App1.applicantPersonID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter App Type");
            App1.applicationTypeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter App status");
            App1.applicationStatus = (clsApplications.enApplicationStatus)(Convert.ToByte(Console.ReadLine()));




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

            dt = clsApplicationTypes.getApplicationTypeRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["applicationTypeID"]},{row["applicationTypeTitle"]},{row["applicationFees"]}");
            }
        }

        //=======================LicenseCLasses======================
        static void printLicenseClasses()
        {
            DataTable dt = new DataTable();

            dt = clsLicenseCLass.getLicenseClassRecords();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["LicenseClassID"]},{row["ClassName"]},{row["ClassDescription"]},{row["MinimumAge"]},{row["DefaultValidityLength"]},{row["ClassFees"]}");
            }
        }

        //=======================Driver======================
        static void printDrivers()
        {
            DataTable dt = new DataTable();

            dt = clsDriver.getDriverRecords(); // Driver kayıtlarını getiren metod

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["DriverID"]},{row["PersonID"]},{row["CreatedByUserID"]},{row["CreatedDate"]}");
            }
        }
        static void addDriver(int personID)
        {

            clsDriver App1 = new clsDriver();
            if (!clsPeople.isPersonExistByID(personID))
            {
                Console.WriteLine("Person with ID {0} could not found!", personID);
                return;
            }
      

            if (App1.save())
            {
                Console.WriteLine("Saved successfully with ID {0}", App1.driverID);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        //static void deleteUser(int userID)
        //{
        //    if (!clsUser.isUserExist(userID))
        //    {
        //        Console.WriteLine("user does not exist");
        //        return;
        //    }

        //    if (clsUser.deleteUser(userID))
        //    {
        //        Console.WriteLine("Deleted successfully");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Something went wrong");
        //    }

        //}

        //static void updateUser(int userID)
        //{

        //    if (!clsUser.isUserExist(userID))
        //    {
        //        Console.WriteLine("userID Does not exist");
        //        return;
        //    }
        //    clsUser App1 = clsUser.findUser(userID);
        //    Console.WriteLine("enter user name");
        //    App1.userName = Console.ReadLine();


        //    if (App1.save())
        //    {
        //        Console.WriteLine("Updated successfully");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Something went wrong");
        //    }
        //}

        static void Main(string[] args)
        {


            //Gelişim Sorularu
            /*
             - mesela database'de firstName null değer kabul etmesin. veriyi database'e
            gönderirken null kontolu dataAccess layer'da yapmalımıyız yoksa sadece Form Üzreinde kontrollerde mi yapacağız.
            **Eğer dataAccess layer'da yapmazsak bu dll'i başka yerde kullandığımızda aynı kontrolleri o arayüzde de yapmamız
            *gerekir yani bu kısım arayüzde bağımlı olur** Meselae bunu önüne şu şelilde geçebiliriz. sadece parametereli
            *const'u public yaparız. BU sayede kullanıc obje oluştutmak için verileri girmek zorunda. Zaten save fonskyionu 
            *objeye bağlı olduğu için anca obje ile çağırılabili. Yani save yapacapımız zaman o objenin tüm verileri düzenli
            *bir şekilde parametreli const tarafından alınmış olur.


            - People sınıfın için Constracter yapısında bir değişiklik var mı? ben birini private birini public yaptım hoca nasıl yapmış?
            - mesela şu an driver eklerken peopleID'de kısmınsa sorun oluıyor. sistemde eğer bir kişi driver'sa o başka tekrara direver olarak eklenemez. Yani sistem bir driver eklerken 1) ilk olarak o Kişi var 2)o kişi zaten driver mı diye kontrol etmeliyiz. Bu kontrol hangi katmanda yapmamız en mantıkı olur? Ben şahsen bunu UI ile sınırlı olmasını istemiyorum. Bir yandan da ya zaten DB böyle bir şeyi eklemeye izin vermez hata olur ve ekleme başarısız olur der bu nedenle tıpkı user eklerken people var mı diye kontolu UI'da yapmıştık, bunuda UI'da yani burda yapalım diyorum.
                     //Read only olan kısımlar var. Mesela application eklerken userID elle girilmesin. bunu sistem o anki hangi kullanıcı aktifse onun ID'sini eklemeli. Bunu gibi readOnly olan durumlar var. mesela app eklerken ödene tutarı sistem direk appTypes'tan getirsin otomatik

            //ŞU AN - app eklerken ödenen tutarı sistem otomatik appType'tan getirmesi için uğraşıyorum. Şu an DB'de bunu yapmaya çalışıyorum. veya  _addNewApplication() fonsktinına bir kod yazıoyor
            //APP laststatus time ne zaman güncellenmeli. Acaba bu UI olduğunda daha mı kolay olur? mesela console ile bunu nasıl yapacağız. UI ile kullanıcı kutularda istrediği kısmı günceller sonra DB'e kayıt ederken eğer status değişmişse anca o zaman status'u değiştiriri.
             */






            addDriver(1);
        }
    }
}



