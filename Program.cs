using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;


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

            if (clsPeople.deletePerson(personID))
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

            clsUser u1 = new clsUser();
            if (!clsPeople.isPersonExistByID(personID))
            {
                Console.WriteLine("Person with ID {0} could not found!", personID);
                return;
            }
            u1.personID = personID;
            u1.userName = userName;
            u1.password = password;
            u1.isActive = isActive;
           
            if (u1.save())
            {
                Console.WriteLine("\n\nSaved with ID " + u1.userID);
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
            clsUser u1 = clsUser.findUser(userID);
            Console.WriteLine("enter user name");
            u1.userName = Console.ReadLine();


            if (u1.save())
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        //========================Countries================================



        static void Main(string[] args)
        {//Adım adım impletemte ederek gidelim ligo gibi

            //başvuruyu silemden kişi sillinmez. Bu yüt FK kısıtlamalrı sistemde olmalı. yani kişinin başvurus varsa o kişi silenemez ilk önce başvuru silinmeli sonra kişi.

            //class isimleri aynı formatta olmalı clsCountriesDataAccess

            /*addPerson(
            //    "n39",
            //    "Ali",
            //    "Mehmet",
            //    "Can",
            //    "kara",
            //    new DateTime(1990, 5, 15),
            //    0,
            //    "İstanbul, Kadıköy",
            //    "05321234567",
            //    "yilmaz@email.com",
            //    1,
            //    "C:\\Users\\Pictures\\ahmet.jpg");
            */
            /* addUser(1, "halil", "1234", true);
            printUsers();
             deleteUser(21);*/

            



        }
    }
}
//Gelişim Sorularu
/*
 - mesela database'de firstName null değer kabul etmesin. veriyi database'e gönderirken null kontolu dataAccess layer'da yapmalımıyız yoksa sadece Form Üzreinde kontrollerde mi yapacağız. **Eğer dataAccess layer'da yapmazsak bu dll'i başka yerde kullandığımızda aynı kontrolleri o arayüzde de yapmamız gerekir yani bu kısım arayüzde bağımlı olur** Meselae bunu önüne şu şelilde geçebiliriz. sadece parametereli const'u public yaparız. BU sayede kullanıc obje oluştutmak için verileri girmek zorunda. Zaten save fonskyionu objeye bağlı olduğu için anca obje ile çağırılabili. Yani save yapacapımız zaman o objenin tüm verileri düzenli bir şekilde parametreli const tarafından alınmış olur.


- People sınıfın için Constracter yapısında bir değişiklik var mı? ben birini private birini public yaptım hoca nasıl yapmış?
 */

