using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Net;
using System.Security.Cryptography;


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

        static void addPerson(string nationalityID,string firstName,string secondName,string thirdName,string lastName,DateTime dateOfBirth,int gender,string address,string phone,string email,int countryID,string imagePath)
        {

            clsPeople p1 = new clsPeople(nationalityID, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, countryID, imagePath);
            if (p1.save())
            {
                Console.WriteLine("\n\nOK");
            }
        }

        static void updatePersonInfo(int personID)
        {
            
            if (!clsPeople.isPersonExist(personID))
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
            if (!clsPeople.isPersonExist(personID))
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
        static void Main(string[] args)
        {//Adım adım impletemte ederek gidelim ligo gibi


            //başvuruyu silemden kişi sillinmez. Bu yüt FK kısıtlamalrı sistemde olmalı. yani kişinin başvurus varsa o kişi silenemez ilk önce başvuru silinmeli sonra kişi.


            //addPerson(
            //    "n34",
            //    "Ahmet",
            //    "Mehmet",
            //    "Can",
            //    "Yılmaz",
            //    new DateTime(1990, 5, 15),
            //    0,
            //    "İstanbul, Kadıköy",
            //    "05321234567",
            //    "ahmet.yilmaz@email.com",
            //    1,
            //    "C:\\Users\\Pictures\\ahmet.jpg"
            //);



            //Gelişim Sorularu
            /*
             - mesela database'de firstName null değer kabul etmesin. veriyi database'e gönderirken null kontolu dataAccess layer'da yapmalımıyız yoksa sadece Form Üzreinde kontrollerde mi yapacağız. **Eğer dataAccess layer'da yapmazsak bu dll'i başka yerde kullandığımızda aynı kontrolleri o arayüzde de yapmamız gerekir yani bu kısım arayüzde bağımlı olur** Meselae bunu önüne şu şelilde geçebiliriz. sadece parametereli const'u public yaparız. BU sayede kullanıc obje oluştutmak için verileri girmek zorunda. Zaten save fonskyionu objeye bağlı olduğu için anca obje ile çağırılabili. Yani save yapacapımız zaman o objenin tüm verileri düzenli bir şekilde parametreli const tarafından alınmış olur.
             */
        }
    }
}


