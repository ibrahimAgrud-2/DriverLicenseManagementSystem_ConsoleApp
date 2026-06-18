using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class UserDataAccess
    {
        public static DataTable getUserRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from users";

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
        public static bool findUserByID(int userID, ref int personID, ref string userName, ref string password, ref
           bool isActive)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from users where userID=@userID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    personID = Convert.ToInt32(read["PersonID"]);
                    userID = Convert.ToInt32(read["userID"]);
                    userName = read["userName"].ToString();
                    password = read["password"].ToString();
                    isActive = Convert.ToBoolean(read["isActive"]);
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

        public static bool isUserExist(int userID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found =1 from users where userID=@userID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", userID);


            try
            {
                connection.Open();

           
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
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


     
        public static int addUser(int personID,  string userName,  string password, 
           bool isActive)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "insert into users values(@personID,@userName,@password,@isActive) Select Scope_Identity();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@personID", personID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@isActive", isActive);

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


        public static bool updateUserInfo(int userID,int personID, string userName, string password,
           bool isActive)
        { 

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "update users set personID=@personID,userName=@userName,password=@password,isActive= @isActive where userID =@userID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@personID", personID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@isActive", isActive);



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


        public static bool deleteUser(int userID)
        {


            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete users where userID=@userID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userID", userID);


            try
            {
                connection.Open();

                //Sorgu sonucu bir sayı geldiyse (ID tek olduğu için sadece bir adet sayı gelir eğer ID varsa) bu demektir ki o ID sistemde var. sayı dışında bir şey gelirse bu demek oluyor ki o kişi sistemde yok.

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 1)
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
