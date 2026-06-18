using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class ApplicationsDataAccess
    {

   
        public static DataTable getApplicationsRecord()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from applications";

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

    
        public static bool findApplication(int applicationID, ref int applicantPersonID, ref DateTime ApplicationDate, ref int applicationTypeID, ref
           byte applicationStatus, ref DateTime LastStatusDate,ref double paidFee,ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from applications where ApplicationID=@applicationID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@applicationID", applicationID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    applicationID = Convert.ToInt32(read["applicationID"]);
                    applicantPersonID =Convert.ToInt32(read["applicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(read["ApplicationDate"]);
                    applicationTypeID = Convert.ToInt32(read["applicationTypeID"]);
                    applicationStatus = Convert.ToByte(read["applicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(read["LastStatusDate"]);
                    paidFee = Convert.ToDouble(read["paidFees"]);
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
    
        public static bool isApplicationExistByID(int applicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from applications where applicationID=@applicationID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@applicationID", applicationID);


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


        //Yeni bir app eklediğimde ben ona tüm bilgieleri paramtere olarak vermem daha doğru olur gibi. Yani UserID'i sistemde
        //aktif kullanıcıdan almasını bu aşamada değil de business layer'da yapmayı uygun gördüm.
        //Zaten userID elle girilen bir şey olmayacağı için o anki kullanıcı IDsi sistemde otomatik çekilir
        public static int addApplication( int applicantPersonID,   int applicationTypeID, 
           byte applicationStatus,  DateTime LastStatusDate,  double paidFee,  int createdByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "insert into applications values (@applicantPersonID,@ApplicationDate,@applicationTypeID,@applicationStatus,@LastStatusDate,@paidFee,@createdByUserID) Select Scope_Identity()";

            SqlCommand cmd = new SqlCommand(query, connection);
            DateTime ApplicationDate = DateTime.Now;

           cmd.Parameters.AddWithValue("@applicantPersonID", applicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@applicationTypeID", applicationTypeID);
            cmd.Parameters.AddWithValue("@applicationStatus", applicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@paidFee", paidFee);
            cmd.Parameters.AddWithValue("@createdByUserID", createdByUserID);


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
        //====Above is checked they are crystal clear===


        public static bool updateApplicationInfo(int applicationID,int applicantPersonID, DateTime ApplicationDate, int applicationTypeID,
           byte applicationStatus, DateTime LastStatusDate, double paidFees, int createdByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "update applications set applicantPersonID=@applicantPersonID,ApplicationDate=@ApplicationDate,applicationTypeID=@applicationTypeID,applicationStatus= @applicationStatus,LastStatusDate=@LastStatusDate,paidFees=@paidFees,createdByUserID=@createdByUserID where applicationID=@applicationID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@applicationID", applicationID);

            cmd.Parameters.AddWithValue("@applicantPersonID", applicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@applicationTypeID", applicationTypeID);
            cmd.Parameters.AddWithValue("@applicationStatus", applicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@paidFees", paidFees);
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


        public static bool deleteApplication(int applicationID)
        {


            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string query = "delete applications where applicationID=@applicationID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@applicationID", applicationID);


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
