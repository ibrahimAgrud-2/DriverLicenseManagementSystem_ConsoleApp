using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class ApplicationTypesDataAccess
    {

        public static DataTable getApplicationTypesRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string sqlQuery = "select * from applicationTypes";

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

        public static bool findApplicationType(int applicationTypeID, ref string applicationTypeTitle, ref double ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string query = "select * from applicationTypes where applicationTypeID=@applicationTypeID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@applicationTypeID", applicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    applicationTypeID = Convert.ToInt32(read["applicationTypeID"]);
                    applicationTypeTitle = read["applicationTypeTitle"].ToString();
                    ApplicationFees = Convert.ToDouble(read["ApplicationFees"]);


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

        public static bool isApplicationTypeExistByID(int applicationTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from applicationTypes where applicationTypeID=@applicationTypeID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@applicationTypeID", applicationTypeID);


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
    }
}
