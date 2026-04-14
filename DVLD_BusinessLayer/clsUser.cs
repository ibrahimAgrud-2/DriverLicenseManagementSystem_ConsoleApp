using System;



namespace DVLD_BusinessLayer
{
    public class clsUser:clsPeople
    {
        public int userID { set; get; }
        public string userName { set; get; }
        public string password { set; get; }
        public bool isActive { set; get; }

       
    }
}
