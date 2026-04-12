using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessLayer
{
    public class clsPeopleDataAccess
    {

        public static DataTable getPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

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

        public static bool findPersonByNationalityNo(ref int personID, string nationalNo, ref string firstName, ref string secondName, ref
       string thirdName, ref string lastName, ref DateTime dateOfBirth, ref
       int gender, ref string address, ref string email, ref string phone, ref
       int countryID, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

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


        public static int addPerson(string nationalNo, string firstName, string secondName,
                   string thirdName, string lastName, DateTime dateOfBirth,
                   int gender, string address, string email, string phone,
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
