using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class LicensesDataAccess
    {
        public static DataTable getLicenseRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from Licenses";

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


        public static bool findLicense(int licenseID,ref int applicationID, ref int driverID, ref int licenseClassID, ref DateTime ıssueDate, ref DateTime LastStatusDate, ref
           string notes,  ref double paidFees, ref bool isActive,ref int issueReason, ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from Licenses where LicenseID=@licenseID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseID", licenseID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                  
                    applicationID = read["ApplicationID"] != DBNull.Value ? Convert.ToInt32(read["ApplicationID"]) : 0;
                        driverID = read["DriverID"] != DBNull.Value ? Convert.ToInt32(read["DriverID"]) : 0;
                        licenseClassID = read["LicenseClass"] != DBNull.Value ? Convert.ToInt32(read["LicenseClass"]) : 0;
                        ıssueDate = read["IssueDate"] != DBNull.Value ? Convert.ToDateTime(read["IssueDate"]) : DateTime.MinValue;
                        LastStatusDate = read["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(read["ExpirationDate"]) : DateTime.MinValue;
                        notes = read["Notes"] != DBNull.Value ? read["Notes"].ToString() : null;
                        paidFees = read["PaidFees"] != DBNull.Value ? Convert.ToDouble(read["PaidFees"]) : 0;
                        isActive = read["IsActive"] != DBNull.Value ? Convert.ToBoolean(read["IsActive"]) : false;
                        issueReason = read["IssueReason"] != DBNull.Value ? Convert.ToInt32(read["IssueReason"]) : 0;
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

        public static bool isLicenseExist(int licenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from Licenses where licenseID=@licenseID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@licenseID", licenseID);


            try
            {
                connection.Open();

                //Sorgu sonucu bir sayı geldiyse (ID tek olduğu için sadece bir adet sayı gelir eğer ID varsa) bu demektir ki o ID sistemde var. sayı dışında bir şey gelirse bu demek oluyor ki o kişi sistemde yok.

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


        public static int addLicense(int applicationID, int driverID, int licenseClass,
      DateTime issueDate, DateTime expirationDate, string notes, double paidFees,
      bool isActive, int issueReason, int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, 
                    ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) 
                    VALUES (@applicationID, @driverID, @licenseClass, @issueDate, 
                    @expirationDate, @notes, @paidFees, @isActive, @issueReason, @createdByUserID);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@applicationID", applicationID);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@licenseClass", licenseClass);
            cmd.Parameters.AddWithValue("@issueDate", issueDate);
            cmd.Parameters.AddWithValue("@expirationDate", expirationDate);

     
            if (string.IsNullOrEmpty(notes))
                cmd.Parameters.AddWithValue("@notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@notes", notes);

            cmd.Parameters.AddWithValue("@paidFees", paidFees);
            cmd.Parameters.AddWithValue("@isActive", isActive);
            cmd.Parameters.AddWithValue("@issueReason", issueReason);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);

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
        public static bool updateLicenseInfo(int licenseID, int driverID, int licenseClass,
    DateTime expirationDate, string notes,double paidFees, bool isActive, int issueReason)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = @"UPDATE Licenses 
                     SET DriverID = @driverID,
                        Notes=@notes,
                         LicenseClass = @licenseClass,
                         ExpirationDate = @expirationDate,
                         PaidFees = @paidFees,
                         IsActive = @isActive,
                            issueReason=@issueReason


                     WHERE LicenseID = @licenseID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseID", licenseID);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@licenseClass", licenseClass);
            cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
            cmd.Parameters.AddWithValue("@isActive", isActive);
            cmd.Parameters.AddWithValue("@paidFees", paidFees);
            cmd.Parameters.AddWithValue("@notes", notes);
            cmd.Parameters.AddWithValue("@issueReason", issueReason);


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


        public static bool deleteLicense(int licenseID)
        {


            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete licenses where licenseID=@licenseID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@licenseID", licenseID);


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
