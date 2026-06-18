using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class InternationalLicenseDataAccess
    {


        public static DataTable getInternationalLicenseRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from InternationalLicenses";

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


        public static bool findInternationalLicenses(int InternationalLicenseID, ref int applicationID, ref int driverID, ref int issuedUsingLocalLicenseID, ref DateTime issueDate, ref DateTime expirationDate,ref bool isActive,ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from InternationalLicenses where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {

                    applicationID = Convert.ToInt32(read["applicationID"]);
                    driverID = Convert.ToInt32(read["driverID"]);
                    issuedUsingLocalLicenseID = Convert.ToInt32(read["issuedUsingLocalLicenseID"]);
                    issueDate = Convert.ToDateTime(read["issueDate"]);
                    expirationDate = Convert.ToDateTime(read["expirationDate"]);
                    isActive = Convert.ToBoolean(read["isActive"]);
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

        public static bool isInternationalLicenseExist(int InternationalLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found =1 from InternationalLicenses where InternationalLicenseID=@InternationalLicenseID   ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


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



        public static int addInternationalLicense(int applicationID,  int driverID, int issuedUsingLocalLicenseID,  DateTime issueDate,  DateTime expirationDate, bool isActive, int createdByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "insert into InternationalLicenses values(@applicationID,@driverID,@issuedUsingLocalLicenseID,@issueDate,@expirationDate,@isActive,@createdByUserID) Select Scope_Identity();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@applicationID", applicationID);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@issuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@issueDate", issueDate);
            cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
            cmd.Parameters.AddWithValue("@isActive", isActive);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);

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


        public static bool updateInternationalLicenseInfo(int InternationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "update InternationalLicenses set applicationID=@applicationID,driverID=@driverID,issuedUsingLocalLicenseID= @issuedUsingLocalLicenseID ,issueDate=@issueDate,expirationDate=@expirationDate,isActive=@isActive,createdByUserID=@createdByUserID  where InternationalLicenseID =@InternationalLicenseID";

            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            cmd.Parameters.AddWithValue("@applicationID", applicationID);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@issuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@issueDate", issueDate);
            cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
            cmd.Parameters.AddWithValue("@isActive", isActive);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);



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


        public static bool deleteInternationalLicense(int InternationalLicenseID)
        {


            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete InternationalLicenses where InternationalLicenseID=@InternationalLicenseID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


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
