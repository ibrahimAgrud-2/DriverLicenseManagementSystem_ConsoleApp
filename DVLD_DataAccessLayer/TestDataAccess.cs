using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class clsTestDataAccess
    {
        public static DataTable getTestRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from Tests";

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

        public static bool findTest(int testID, ref int testAppointmentID, ref int testResult,
            ref string notes, ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from Tests where TestID = @testID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testID", testID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    testAppointmentID = read["TestAppointmentID"] != DBNull.Value ? Convert.ToInt32(read["TestAppointmentID"]) : 0;
                    testResult = read["TestResult"] != DBNull.Value ? Convert.ToInt32(read["TestResult"]) : 0;
                    notes = read["Notes"] != DBNull.Value ? read["Notes"].ToString() : null;
                    createdByUserID = read["CreatedByUserID"] != DBNull.Value ? Convert.ToInt32(read["CreatedByUserID"]) : 0;

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

        public static bool isTestExist(int testID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found = 1 from Tests where TestID = @testID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testID", testID);

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
                return false;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public static int addTest(int testAppointmentID, int testResult, string notes, int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) 
                     VALUES (@testAppointmentID, @testResult, @notes, @createdByUserID);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
            cmd.Parameters.AddWithValue("@testResult", testResult);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);

            if (string.IsNullOrEmpty(notes))
                cmd.Parameters.AddWithValue("@notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@notes", notes);

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    return insertedID;
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

        public static bool updateTest(int testID, int testAppointmentID, int testResult, string notes)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"UPDATE Tests 
                     SET TestAppointmentID = @testAppointmentID,
                         TestResult = @testResult,
                         Notes = @notes
                     WHERE TestID = @testID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@testID", testID);
            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
            cmd.Parameters.AddWithValue("@testResult", testResult);

            if (string.IsNullOrEmpty(notes))
                cmd.Parameters.AddWithValue("@notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@notes", notes);

            try
            {
                connection.Open();
                int affectedRowsNumber = cmd.ExecuteNonQuery();
                return (affectedRowsNumber == 1);
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

        public static bool deleteTest(int testID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete Tests where TestID = @testID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testID", testID);

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
                return false;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }
    }
}