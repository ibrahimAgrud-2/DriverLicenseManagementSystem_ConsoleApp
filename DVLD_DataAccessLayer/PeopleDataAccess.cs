using System;
using System.Data;
using System.Data.SqlClient;



namespace DVLD_DataAccessLayer
{
    public class PeopleDataAccess
    {

        public static DataTable getPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from people";

            SqlCommand cmd = new SqlCommand(sqlQuery, connection);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                    if (read.HasRows)
                    {
                        dt.Load(read);
                    }
               
                read.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
           


            return dt;
        }
        public static  bool findPersonByID(int personID, ref string nationalNo, ref string firstName, ref string secondName, ref
           string thirdName, ref string lastName, ref DateTime dateOfBirth, ref
           int gender, ref string address, ref string email, ref string phone, ref
           int countryID, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from People where PersonID=@personID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                     personID = Convert.ToInt32(read["PersonID"]);
                     nationalNo = read["nationalNo"].ToString();
                     firstName = read["firstName"].ToString();
                     secondName = read["secondName"].ToString();
                     thirdName = read["thirdName"].ToString();
                     lastName = read["lastName"].ToString();
                     dateOfBirth = Convert.ToDateTime(read["dateOfBirth"]);
                     gender = Convert.ToInt32(read["gender"]);
                     address = read["address"].ToString();
                     email = read["email"].ToString();
                     phone = read["phone"].ToString();
                     countryID = Convert.ToInt32(read["nationalityCountryID"]);
                     imagePath = read["imagePath"].ToString();

                    if (read["ImagePath"] != DBNull.Value)
                    {
                        imagePath = (string)read["ImagePath"];
                    }
                    else
                    {
                        imagePath = "";
                    }

                    return true;
                }
                else
                {
                   
                    return false;
                }
          

            }
            catch (Exception)
            {
                
                return false;
            }
            finally
            {
                connection.Close();
            }
            
            
        }

        public static bool findPersonByNationalNo(ref int personID, string nationalNo, ref string firstName, ref string secondName, ref
       string thirdName, ref string lastName, ref DateTime dateOfBirth, ref
       int gender, ref string address, ref string email, ref string phone, ref
       int countryID, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from People where NationalNo=@nationalNo";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {

                    personID = Convert.ToInt32(read["PersonID"]);
                    nationalNo = read["nationalNo"].ToString();
                    firstName = read["firstName"].ToString();
                    secondName = read["secondName"].ToString();
                    thirdName = read["thirdName"].ToString();
                    lastName = read["lastName"].ToString();
                    dateOfBirth = Convert.ToDateTime(read["dateOfBirth"]);
                    gender = Convert.ToInt32(read["gender"]);
                    address = read["address"].ToString();
                    email = read["email"].ToString();
                    phone = read["phone"].ToString();
                    countryID = Convert.ToInt32(read["nationalityCountryID"]);
                    imagePath = read["imagePath"].ToString();
                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                connection.Close();

            }

            return false;
        }


        public static bool isPersonExistByID(int personID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.
             
            string query = "select found =1 from people where PersonID=@PersonID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@personID", personID);
        

            try
            {
                connection.Open();
          
                //Sorgu sonucu bir sayı geldiyse (ID tek olduğu için sadece bir adet sayı gelir eğer ID varsa) bu demektir ki o ID sistemde var. sayı dışında bir şey gelirse bu demek oluyor ki o kişi sistemde yok.

                object result = cmd.ExecuteScalar();
                if (result!=null&&int.TryParse(result.ToString(), out int value))
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false; ;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }
        //nationalNo yani TC eşsiz olacağı için yeni kişi eklemden önce sistemde o TC katıtlımı diye kontrol edebilmek için bu fonk kullanıyoruz.
  
        public static bool isPersonExistByNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from people where NationalNo=@nationalNo";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nationalNo", NationalNo);


            try
            {
                connection.Open();


                object result = cmd.ExecuteScalar();
                if (result != null&&int.TryParse(result.ToString(), out int value))
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false; ;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public static int addPerson(string nationalNo, string firstName, string secondName,
                   string thirdName, string lastName, DateTime dateOfBirth,
                   int gender, string address, string email, string phone,
                   int countryID, string imagePath)
        {
          
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "IF not exists (select personID from people where NationalNo=@nationalNo) begin insert into people values (@nationalNo,@firstName,@secondName,@thirdName,@lastName,@dateOfBirth,@gender,@address,@email,@phone,@nationalityCountryID,@imagePath) Select Scope_Identity() end;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@firstName",firstName);
            cmd.Parameters.AddWithValue("@secondName", secondName);
            cmd.Parameters.AddWithValue("@thirdName", thirdName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phone);
             cmd.Parameters.AddWithValue("@nationalityCountryID", countryID);
            cmd.Parameters.AddWithValue("@nationalNo", nationalNo);
           

            if (imagePath != "" && imagePath != null)
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
            else
                cmd.Parameters.AddWithValue("@imagePath", System.DBNull.Value);


            try
            {
                connection.Open();

                //Sql içinde sorgu burada çalışır.
                object result = cmd.ExecuteScalar();
                
                if (result != null && int.TryParse(result.ToString(), out int inserted))
                {
                    return inserted;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {

                return -1;
            }
            finally
            {
                connection.Close();
            }

        }


        public static bool updatePersonInfo(int personID ,string nationalNo, string firstName, string secondName,
          string thirdName, string lastName, DateTime dateOfBirth,
          int gender, string address, string email, string phone,
          int countryID, string imagePath)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "update people set nationalNo=@nationalNo,firstName=@firstName,secondName=@secondName,thirdName= @thirdName,lastName=@lastName,dateOfBirth=@dateOfBirth,gender=@gender,address=@address,email=@email,phone = @phone,nationalityCountryID=@nationalityCountryID,imagePath=@imagePath where personID =@personID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@personID", personID);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@secondName", secondName);
            cmd.Parameters.AddWithValue("@thirdName", thirdName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@nationalityCountryID", countryID);
            cmd.Parameters.AddWithValue("@nationalNo", nationalNo);
            if (imagePath == string.Empty)
            {
                cmd.Parameters.AddWithValue("@imagePath", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@imagePath", imagePath);

            }


            try
            {
                connection.Open();
                //affectedRowsNumber==1 çünkü her seferinde sadece 1 satır günnceleyebiliriz onun dışındaki tüm durumlar beklenmedik durum.
                int affectedRowsNumber = cmd.ExecuteNonQuery();
                if (affectedRowsNumber == 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false; ;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }


        public static bool deletePerson(int personID)
        {
            
            
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete people where PersonID=@personID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@personID", personID);


            try
            {
                connection.Open();

                //Sorgu sonucu bir sayı geldiyse (ID tek olduğu için sadece bir adet sayı gelir eğer ID varsa) bu demektir ki o ID sistemde var. sayı dışında bir şey gelirse bu demek oluyor ki o kişi sistemde yok.

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows==1)
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false; ;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

    }
}
