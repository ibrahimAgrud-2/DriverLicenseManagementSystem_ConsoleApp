using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class clsPeopleDataAccess
    {

        public static DataTable getAllPersonRecords()
        {
            DataTable dtRecodrs = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string sqlQuery = "select * from people";

            SqlCommand cmd = new SqlCommand(sqlQuery, connection);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                    if (read.HasRows)
                    {
                        dtRecodrs.Load(read);
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
           


            return dtRecodrs;
        }

        public static int addPerson(string nationalNo, string firstName, string secondName,
                   string thirdName, string lastName, DateTime dateOfBirth,
                   string gender, string address, string email, string phone,
                   int countryID, string imagePath)
        {
          
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "insert into people values (@nationalNo,@firstName,@secondName,@thirdName,@lastName,@dateOfBirth,@gender,@address,@email,@phone,@nationalityCountryID,@imagePath);Select Scope_Identity();";

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



            if (imagePath==string.Empty)
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
    }
}
