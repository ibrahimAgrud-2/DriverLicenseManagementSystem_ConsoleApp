using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class clsDriverDataAccess
    {

        public static DataTable getDriverRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from Drivers";

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
       
        public static bool findDriver(int driverID, ref int personID, ref int createdByUserID, ref DateTime createdDate)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from users where DriverID=@DriverID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    personID = Convert.ToInt32(read["PersonID"]);
                    createdDate = Convert.ToDateTime(read["createdDate"]);
                    createdByUserID = Convert.ToInt32(read["createdByUserID"]);
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

        public static bool isDriverExistByDriverID(int driverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found =1 from Drivers where driverID=@driverID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@driverID", driverID);


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

        public static bool isDriverExistByPersonID(int personID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found =1 from Drivers where personID=@personID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@personID", personID);


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


        public static int addDriver(int personID,  int createdByUserID,  DateTime createdDate)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "insert into drivers values(@personID,@createdByUserID,@createdDate) Select Scope_Identity();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@personID", personID);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            cmd.Parameters.AddWithValue("@createdDate", createdDate);

            try
            {
                connection.Open();

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


        public static bool updateDriverInfo(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "update drivers set personID=@personID,createdByUserID=@createdByUserID,createdDate= @createdDate where driverID =@driverID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@personID", personID);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            cmd.Parameters.AddWithValue("@createdDate", createdDate);



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


        public static bool deleteDriver(int driverID)
        {


            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete drivers where driverID=@driverID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@driverID", driverID);


            try
            {
                connection.Open();


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
