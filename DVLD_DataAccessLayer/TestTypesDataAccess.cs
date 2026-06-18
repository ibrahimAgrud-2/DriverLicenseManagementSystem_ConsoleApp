using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class TestTypesDataAccess
{
    public static DataTable getTestTypesRecords()
    {
        DataTable dt = new DataTable();

        SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
        string sqlQuery = "select * from TestTypes";

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

    public static bool findTestType(int testTypeID, ref string testTypeTitle, ref string testTypeDescription, ref double testTypeFees)
    {
        SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

        string query = "select * from TestTypes where TestTypeID = @testTypeID";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@testTypeID", testTypeID);

        try
        {
            connection.Open();
            SqlDataReader read = cmd.ExecuteReader();

            if (read.Read())
            {
                testTypeID = Convert.ToInt32(read["TestTypeID"]);
                testTypeTitle = read["TestTypeTitle"].ToString();
                testTypeDescription = read["TestTypeDescription"].ToString();
                testTypeFees = Convert.ToDouble(read["TestTypeFees"]);

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

    public static bool isTestTypeExistByID(int testTypeID)
    {
        SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

        string query = "select found = 1 from TestTypes where TestTypeID = @testTypeID";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@testTypeID", testTypeID);

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
}