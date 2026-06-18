using DVLD_DataAccessLayer;
using System;
using System.Data;



namespace DVLD_BusinessLayer
{
    public class User
    {
        public int userID { set; get; }
        public int personID { set; get; }
        public string userName { set; get; }
        public string password { set; get; }
        public bool isActive { set; get; }

        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode mode;


        public User()
        {
            this.userName = "";
            this.personID = -1;
            this.userID = -1;
            this.password = "";
            this.isActive = false;
            this.mode = enMode.enAddNew;
        }

        private User(int userID, int personID, string userName, string password, bool isActive)
        {

            this.personID = personID;
            this.userID = userID;
            this.userName = userName;
            this.password = password;
            this.isActive = isActive;
            this.mode = enMode.enUpdate;
        }
       public static DataTable getUserRecords()
        {
            DataTable dt = new DataTable();

            dt = UserDataAccess.getUserRecords();
            return dt;
        }

        public static User findUser(int userID)
        {

            string userName = "", password = "";
            int personID = -1;
            bool isActive=false;


            if (UserDataAccess.findUserByID(userID, ref personID, ref userName, ref password, ref isActive))
            {
                return new User(userID,  personID,  userName,  password,  isActive);

            }
            return null;
        }


        private bool _addNewUser()
        {
            this.userID = UserDataAccess.addUser(this.personID, this.userName, this.password, this.isActive);
            return (this.userID != -1);

        }
        private bool _updateUserInfo()
        {

            return UserDataAccess.updateUserInfo(this.userID, this.personID, this.userName, this.password, this.isActive);
        }


        public static bool isUserExist(int userID)
        {
            return UserDataAccess.isUserExist(userID);
        }
       
        public static bool deleteUser(int userID)
        {
            if (isUserExist(userID))
            {
                return UserDataAccess.deleteUser(userID);
            }
            return false;

        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.enAddNew:
                    return _addNewUser();

                case enMode.enUpdate:
                    return _updateUserInfo();
                default:
                    return false;
            }
        }

       
    }
}
