using DVLD_DataAccessLayer;
using System;
using System.Data;



namespace DVLD_BusinessLayer
{
    public class clsPeople
    {
  
        public int personID { set; get; }
        public string nationalNo { set; get; }
        public string firstName { set; get; }
        public string secondName { set; get; }
        public string thirdName { set; get; }
        public string lastName { set; get; }
        public DateTime dateOfBirth { set; get; }
        public int gender { set; get; }
        public string address { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public int countryID { set; get; }
        public string imagePath { set; get; }

        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;


        //static olmalı çünkü obje oluşturulmadan erişebilmeliyiz.
        public static DataTable getAllPersonRecords()
        {
            return clsPeopleDataAccess.getPeople();
        }

        //veriyi yani bilgileri ben elle girdiğimi için bu kişi DB'de yoktur anlamına geltir yani bu yüzden parametreli const mode add olmalı ve bence sadece bu const dışardan erişilebilmeli ki dışardan boş bir obje oluşturup DB'ye kayıt edemesinler.
        public clsPeople(string nationalNo, string firstName, string secondName,
           string thirdName, string lastName, DateTime dateOfBirth,
           int gender, string address, string email, string phone,
           int countryID, string imagePath)
        {

            this.nationalNo = nationalNo;
            this.firstName = firstName;
            this.secondName = secondName;
            this.thirdName = thirdName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.countryID = countryID;
            this.imagePath = imagePath;
            this.mode = enMode.enAddNew;
        }

        private bool _addNewRecord()
        {
            this.personID = clsPeopleDataAccess.addPerson(this.nationalNo, this.firstName, this.secondName, this.thirdName, this.lastName, this.dateOfBirth, this.gender, this.address, this.email, this.phone, this.countryID, this.imagePath);

            return (this.personID != -1);
        }

        //bu fonkisyonun parameter almasında gerek yok çünkü ben bir objeyi istediğimi kısımları güncellerim sonra save ile update yaparım.
  


        //find'da eğer jayıt yoksa boş obje döndermek için kullanmak için işe yarayabilir diye oluşturdum.
        //Find parametreli cons kullanamam çünkü parametereli const sadece sıfırdan veri eklemek için kullanılıyıor.
        
        //Bence burada mode olmamalı. Çünkü update, add yapabilceğimizi düzgün obje yok ki. Bunula boş obje oluşturabiliriz ve boş obje add veya updete olamaz.
        private clsPeople()
        {

            this.nationalNo = "";
            this.firstName = "";
            this.secondName = "";
            this.thirdName = "";
            this.lastName =  "";
            this.dateOfBirth = DateTime.Now;
            this.gender = 0;
            this.address =   "";
            this.email =   "";
            this.phone =  "";
            this.countryID = -1;
            this.imagePath = ""; ;
            
        }

        //Find ile bulunan mode update olsun çünkü artık obje var ve add'lik bir durum kalmamış. Artık yaparsak update yaparız
        public static clsPeople findPersonByID(int personID)
        {
            
            string nationalNo="", firstName = "", secondName = "",thirdName = "",lastName = "", address = "", email = "", phone = "", imagePath = "";
            DateTime dateOfBirth=DateTime.Now;
            int gender=-1,countryID=-1;
            

            bool findResult = clsPeopleDataAccess.findPersonByID( personID, ref nationalNo, ref  firstName, ref  secondName, ref  thirdName, ref  lastName, ref  dateOfBirth, ref  gender, ref  address, ref  email, ref  phone, ref  countryID, ref  imagePath);

            clsPeople p1 = new clsPeople();
            if (findResult)
            {
                p1.personID = personID;
                p1.nationalNo = nationalNo;
                p1.firstName = firstName;
                p1.secondName = secondName;
                p1.thirdName = thirdName;
                p1.dateOfBirth = dateOfBirth;
                p1.email = email;
                p1.phone = phone;
                p1.address = address;
                p1.gender = gender;
                p1.countryID = countryID;
                p1.imagePath = imagePath;
                p1.mode = enMode.enUpdate;
              
            }
            return  p1;

        }

        public static clsPeople findPersonByNationalNo(string nationalNo)
        {

            string firstName = "", secondName = "", thirdName = "", lastName = "", address = "", email = "", phone = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int personID=-1, gender = -1, countryID = -1;


            bool findResult = clsPeopleDataAccess.findPersonByNationalityNo( ref personID, nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref email, ref phone, ref countryID, ref imagePath);

            clsPeople p1 = new clsPeople();
            if (findResult)
            {
                p1.personID = personID;
                p1.nationalNo = nationalNo;
                p1.firstName = firstName;
                p1.secondName = secondName;
                p1.thirdName = thirdName;
                p1.dateOfBirth = dateOfBirth;
                p1.email = email;
                p1.phone = phone;
                p1.address = address;
                p1.gender = gender;
                p1.countryID = countryID;
                p1.imagePath = imagePath;
                p1.mode = enMode.enUpdate;

            }
            return p1;

        }


        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewRecord();
                   
                case enMode.enUpdate:
                    return false;
                default:
                    return false;
            }
        }

    }
}
