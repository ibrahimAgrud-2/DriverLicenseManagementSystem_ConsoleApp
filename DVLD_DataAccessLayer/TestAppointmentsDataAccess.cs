using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class TestAppointmentsDataAccess
    {
        public static DataTable getTestAppointmentsRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from TestAppointments";

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

        public static bool findTestAppointment(int testAppointmentID, ref int testTypeID, ref int localDrivingLicenseApplicationID,
            ref DateTime appointmentDate, ref double paidFees, ref int createdByUserID,
            ref bool isLocked, ref int retakeTestApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from TestAppointments where TestAppointmentID = @testAppointmentID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    testTypeID = read["TestTypeID"] != DBNull.Value ? Convert.ToInt32(read["TestTypeID"]) : 0;
                    localDrivingLicenseApplicationID = read["LocalDrivingLicenseApplicationID"] != DBNull.Value ? Convert.ToInt32(read["LocalDrivingLicenseApplicationID"]) : 0;
                    appointmentDate = read["AppointmentDate"] != DBNull.Value ? Convert.ToDateTime(read["AppointmentDate"]) : DateTime.MinValue;
                    paidFees = read["PaidFees"] != DBNull.Value ? Convert.ToDouble(read["PaidFees"]) : 0;
                    createdByUserID = read["CreatedByUserID"] != DBNull.Value ? Convert.ToInt32(read["CreatedByUserID"]) : 0;
                    isLocked = read["IsLocked"] != DBNull.Value ? Convert.ToBoolean(read["IsLocked"]) : false;
                    retakeTestApplicationID = read["RetakeTestApplicationID"] != DBNull.Value ? Convert.ToInt32(read["RetakeTestApplicationID"]) : 0;

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

        public static bool isTestAppointmentExist(int testAppointmentID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select found = 1 from TestAppointments where TestAppointmentID = @testAppointmentID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);

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

        public static int addTestAppointment(int testTypeID, int localDrivingLicenseApplicationID,
            DateTime appointmentDate, double paidFees, int createdByUserID,
            bool isLocked, int retakeTestApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, 
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID) 
                    VALUES (@testTypeID, @localDrivingLicenseApplicationID, @appointmentDate, 
                    @paidFees, @createdByUserID, @isLocked, @retakeTestApplicationID);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@testTypeID", testTypeID);
            cmd.Parameters.AddWithValue("@localDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@appointmentDate", appointmentDate);
            cmd.Parameters.AddWithValue("@paidFees", paidFees);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            cmd.Parameters.AddWithValue("@isLocked", isLocked);

            if (retakeTestApplicationID == 0)
                cmd.Parameters.AddWithValue("@retakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@retakeTestApplicationID", retakeTestApplicationID);

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

        public static bool updateTestAppointment(int testAppointmentID, int testTypeID,
            DateTime appointmentDate, double paidFees, bool isLocked, int retakeTestApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"UPDATE TestAppointments 
                     SET TestTypeID = @testTypeID,
                         AppointmentDate = @appointmentDate,
                         PaidFees = @paidFees,
                         IsLocked = @isLocked,
                         RetakeTestApplicationID = @retakeTestApplicationID
                     WHERE TestAppointmentID = @testAppointmentID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
            cmd.Parameters.AddWithValue("@testTypeID", testTypeID);
            cmd.Parameters.AddWithValue("@appointmentDate", appointmentDate);
            cmd.Parameters.AddWithValue("@paidFees", paidFees);
            cmd.Parameters.AddWithValue("@isLocked", isLocked);

            if (retakeTestApplicationID == 0)
                cmd.Parameters.AddWithValue("@retakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@retakeTestApplicationID", retakeTestApplicationID);

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

        public static bool deleteTestAppointment(int testAppointmentID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete TestAppointments where TestAppointmentID = @testAppointmentID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);

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