using DVLD_BusinessLayer;
using System;
using System.Data;


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

        static clsPeople readPersonData()
        {
            clsPeople p1 = new clsPeople();


            Console.WriteLine("NationalityNo:");
            p1.nationalNo = Console.ReadLine();

            Console.WriteLine("First Name:");
            p1.firstName = Console.ReadLine();

            Console.WriteLine("Second Name:");
            p1.secondName = Console.ReadLine();

            Console.WriteLine("Third Name:");
            p1.thirdName = Console.ReadLine();

            Console.WriteLine("Last Name:");
            p1.lastName = Console.ReadLine();

            Console.WriteLine("Date of Birth (yyyy-MM-dd):");
            p1.dateOfBirth = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Gender (M(0)/F(1)):");
            p1.gender = Console.ReadLine();

            Console.WriteLine("Address:");
            p1.address = Console.ReadLine();

            Console.WriteLine("Email:");
            p1.email = Console.ReadLine();

            Console.WriteLine("Phone:");
            p1.phone = Console.ReadLine();

            Console.WriteLine("Country ID:");
            p1.countryID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Image Path:");
            p1.imagePath = Console.ReadLine();

            return p1;
        }

        static void Main(string[] args)
        {
            //Mode konusu
            //hangi durumlarda mode add hang durumlarda update
            //parametereli const'ta mode var mı daha dogrusu parametereli const olmalı mi, dışrdan erişilebili mi? 


            //Adım adım implemente ederek gidelim.


            printAllPeopleRecords();
        }
    }
}
