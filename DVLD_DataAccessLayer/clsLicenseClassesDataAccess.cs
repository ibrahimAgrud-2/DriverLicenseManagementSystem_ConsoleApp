using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class clsLicenseClassesDataAccess
    {
        public static DataTable getLicenseClassesRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string sqlQuery = "select * from LicenseClasses";

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
        public static bool findLicenseClass(int licenseClassID, ref string className,ref string classDescription,ref int minimumAge,ref int defaultValidityLength, double classFee)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from LicenseClasses where LicenseClassID=@LicenseClassID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseClassID", licenseClassID);


            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    licenseClassID = read["licenseClassID"] != DBNull.Value ? Convert.ToInt32(read["licenseID"]) : 0;
                    className = read["className"].ToString();
                    classDescription = read["classDescription"].ToString();
                    minimumAge = read["minimumAge"] != DBNull.Value ? Convert.ToInt32(read["minimumAge"]) : 0;
                    defaultValidityLength = read["defaultValidityLength"] != DBNull.Value ? Convert.ToInt32(read["defaultValidityLength"]) : 0;
                    classFee = read["classFee"] != DBNull.Value ? Convert.ToDouble(read["classFee"]) : 0.0
                    ;


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

        public static bool isLicenseClass(int licenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select found =1 from LicenseClassed where licenseClassID=@licenseClassID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@licenseClassID", licenseClassID);


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

       

    }
}
