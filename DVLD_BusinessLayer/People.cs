using DVLD_DataAccessLayer;
using System;
using System.Data;



namespace DVLD_BusinessLayer
{
    public class People
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
            return PeopleDataAccess.getPeople();
        }

        //1.Yöntem const'u public yaparız ve ID paramtersi eklemeyiz. Bu saydede kullanıcı dışardan obje oluşturacağı zaman illa uygun verileri girmek zorunda kalır. mode add olur çünkü dışardan oluşturulan o obje DB'de yok ilk defa oluşturuluyor, zaten ID auto number.
        //2.Bir yöntem bunu private yaparız ve ID parametresi de ekleriz. Bu sayede DB'den aldığımız satırları program içinde direk bu const ile temsil etmiş oluruz. 2.Yöntem'den gidelim.
        private People(int personID,string nationalNo, string firstName, string secondName,
           string thirdName, string lastName, DateTime dateOfBirth,
           int gender, string address, string email, string phone,
           int countryID, string imagePath)
        {

            this.personID = personID;
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
            this.mode = enMode.enUpdate;
        }



        private bool _addNewRecord()
        {
            this.personID = PeopleDataAccess.addPerson(this.nationalNo, this.firstName, this.secondName, this.thirdName, this.lastName, this.dateOfBirth, this.gender, this.address, this.email, this.phone, this.countryID, this.imagePath);

            return (this.personID != -1);
        }




        
        //Mode add çünkü sıfırdan obje bu const ile oluşturuluyor.
        public  People()
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
            this.mode = enMode.enAddNew;
            
        }

        //Find ile bulunan obje parametreli const ile oluşturulduğu için modu update olsun çünkü artık obje var ve add'lik bir durum kalmamış. Artık yaparsak update yaparız
        public static People findPersonByID(int personID)
        {
            
            string nationalNo="", firstName = "", secondName = "",thirdName = "",lastName = "", address = "", email = "", phone = "", imagePath = "";
            DateTime dateOfBirth=DateTime.Now;
            int gender=-1,countryID=-1;
            
           
            if (PeopleDataAccess.findPersonByID(personID, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref email, ref phone, ref countryID, ref imagePath))
            {
                return new People(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, email, phone, countryID, imagePath);
              
            }
            return null;
        }

        public static People findPersonByNationalNo(string nationalNo)
        {

            string firstName = "", secondName = "", thirdName = "", lastName = "", address = "", email = "", phone = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int personID=-1, gender = -1, countryID = -1;


    

            if (PeopleDataAccess.findPersonByNationalNo(ref personID, nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref email, ref phone, ref countryID, ref imagePath))
            {
                return new People(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, email, phone, countryID, imagePath);
            }
            return null;
        }


        //bu fonkisyonun parameter almasında gerek yok çünkü ben bir objeyinin istediğimi kısımlarını güncellerim sonra save ile update yaparım.
        private bool _updateInfo()
        {
          
            return PeopleDataAccess.updatePersonInfo(this.personID, this.nationalNo, this.firstName, this.secondName, this.thirdName, this.lastName, this.dateOfBirth, this.gender, this.address, this.email, this.phone, this.countryID, this.imagePath);
        }

        //ID vermelisin çünkü bu fonk obje olmadan çağıralacak.
        public static bool isPersonExistByID(int personID)
        {
            return PeopleDataAccess.isPersonExistByID(personID);
        }
        //TC sistemde var mı yoku kontrol edeceğiz bu sayede aynı TC ile sisteme kayıt olunmasın. Şu anlık DB'de bunu nasıl kontrol ederiz bilemiyorum. Veya bu yöntem daha hızlı olduğu için bunu tercih ederim.
        public static bool isPersonExistByNationalNo(string NationalNo)
        {
            return PeopleDataAccess.isPersonExistByNationalNo(NationalNo);
        }
        //bir objeyi silmek için onu ayrıyeten DB'den programam yüklemem gerek yok bu yüzden static yapıp sadece ID alarak silmek istedim. static olmayadabilirdi. Bu durumda fonk public olur ve obje.delete diyeceğimiz için ID'İ parametre olarak bu fonksyion'a vermemiz gerekmezdi, this kullanarak ID alırdık (çünkü program o anki obje üzerin olurdu)

        //Buraya şöyle bir durum eklenmeli eğer bu kişi sistemde bir işleme başvuru yapmışsa bu kişiyi direk silemezsin. İlk önce o başvuruyu silmelisin sonra kişiyi silebilirsin. Çünkü başvurular tablosun'dan bu tabloya FK var sonuç olarak eğer kişiyi silersen bu sefer başvuru bilgisi eksik olur yani başvuruyu kimin yaptıığ bilinmez.
        public static bool delete(int personID)
        {
            if (isPersonExistByID(personID))
            { 
                return PeopleDataAccess.deletePerson(personID);
            }
            return false;
          
        }
        
        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewRecord();
                   
                case enMode.enUpdate:
                    return _updateInfo();
                default:
                    return false;
            }
        }


        public bool sendEmail(string from ,string to,string body)
        {
            return false;
        }
        public void call(string phoneNumber)
        {
            return;
        }

    }
}
