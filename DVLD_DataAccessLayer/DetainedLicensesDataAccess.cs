using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class DetainedLicensesDataAccess
    {
        public static DataTable getDetainedLicenseRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from DetainedLicenses";

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

 
        public static bool findDetainedLicense(int detainID, ref int licenseID, ref DateTime detainDate, ref double fineFees, ref int createdByUserID, ref bool isReleased, ref DateTime releaseDate, ref int releasedByUserID, ref int releaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from DetainedLicenses where DetainID=@detainID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@detainID", detainID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    licenseID = read["licenseID"] != DBNull.Value ? Convert.ToInt32(read["licenseID"]) : 0;
                    detainDate = read["DetainDate"] != DBNull.Value ? Convert.ToDateTime(read["DetainDate"]) : DateTime.MinValue;
                    fineFees = read["FineFees"] != DBNull.Value ? Convert.ToDouble(read["FineFees"]) : 0;
                    createdByUserID = read["CreatedByUserID"] != DBNull.Value ? Convert.ToInt32(read["CreatedByUserID"]) : 0;
                    isReleased = read["IsReleased"] != DBNull.Value ? Convert.ToBoolean(read["IsReleased"]) : false;
                    releaseDate = read["ReleaseDate"] != DBNull.Value ? Convert.ToDateTime(read["ReleaseDate"]) : DateTime.MinValue;
                    releasedByUserID = read["ReleasedByUserID"] != DBNull.Value ? Convert.ToInt32(read["ReleasedByUserID"]) : 0;
                    releaseApplicationID = read["ReleaseApplicationID"] != DBNull.Value ? Convert.ToInt32(read["ReleaseApplicationID"]) : 0;

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

        public static bool isDetainedLicenseExist(int detainID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from DetainedLicenses where detainID=@detainID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@detainID  ", detainID);


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


        public static int addDetainedLicense(int licenseID,  DateTime detainDate,  double fineFees,  int createdByUserID,  bool isReleased,  DateTime releaseDate,  int releasedByUserID,  int releaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"INSERT INTO DetainedLicenses (licenseID, detainDate, fineFees, createdByUserID, 
                    isReleased, releaseDate, releasedByUserID, releaseApplicationID) 
                    VALUES (@licenseID, @detainDate, @fineFees, @createdByUserID, 
                    @isReleased, @releaseDate, @releasedByUserID, @releaseApplicationID);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseID", licenseID);
            cmd.Parameters.AddWithValue("@detainDate", detainDate);
            cmd.Parameters.AddWithValue("@fineFees", fineFees);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            cmd.Parameters.AddWithValue("@isReleased", isReleased);
            cmd.Parameters.AddWithValue("@releaseDate", releaseDate);
            cmd.Parameters.AddWithValue("@releasedByUserID", releasedByUserID); 
            cmd.Parameters.AddWithValue("@releaseApplicationID", releaseApplicationID);



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


        //Şimdi licesnes tablosunda update yaparken bazı alanları değişrimemiz gerekebilir. Mesela userID değişmemesi gerekir çünkü sistem o an kim güncelleme yapmışsa onu eklere hata olamaz, ama notes değişebilir.
        public static bool updateDetainedLicense(int detainID,int licenseID, DateTime detainDate, double fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"UPDATE DetainedLicenses 
                     SET licenseID = @licenseID,
                        detainDate=@detainDate,
                         fineFees = @fineFees,
                         createdByUserID = @createdByUserID,
                         isReleased = @isReleased,
                         releaseDate = @releaseDate,
                            releasedByUserID=@releasedByUserID,
                             releaseApplicationID = @releaseApplicationID
                     WHERE DetainID = @DetainID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@detainID", detainID);
            cmd.Parameters.AddWithValue("@licenseID", licenseID);
            cmd.Parameters.AddWithValue("@detainDate", detainDate);
            cmd.Parameters.AddWithValue("@fineFees", fineFees);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            cmd.Parameters.AddWithValue("@isReleased", isReleased);
            cmd.Parameters.AddWithValue("@releaseDate", releaseDate);
            cmd.Parameters.AddWithValue("@releasedByUserID", releasedByUserID);
            cmd.Parameters.AddWithValue("@releaseApplicationID", releaseApplicationID);


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

        //createdByUseID, dates(%80),releasedByUserID
        public static bool deleteDetainedLicense(int detainedLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete DetainedLicenses where detainedLicenseID=@detainedLicenseID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@detainedLicenseID", detainedLicenseID);


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
