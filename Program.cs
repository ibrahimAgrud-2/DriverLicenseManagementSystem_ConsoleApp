using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Net;


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

  
        static void Main(string[] args)
        {
            //Mode konusu
            //hangi durumlarda mode add hang durumlarda update
            //parametereli const'ta mode var mı daha dogrusu parametereli const olmalı mi, dışrdan erişilebili mi? 


            //Adım adım implemente ederek gidelim.




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


