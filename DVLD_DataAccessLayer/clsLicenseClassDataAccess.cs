using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class clsLicenseClassDataAccess
    {

        public static DataTable getLicenseClassesRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string sqlQuery = "select  *   from LicenseClasses";

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

        public static bool findLicenseClass(int licenseClassID, ref string className, ref string classDescription, ref int MinimumAge, ref int defaultValidityLength, ref double classFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from licenseClasses where licenseClassID=@licenseClassID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseClassID", licenseClassID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    defaultValidityLength = Convert.ToInt32(read["defaultValidityLength"]);
                    MinimumAge = Convert.ToInt32(read["MinimumAge"]);
                    className = read["className"].ToString();
                    classDescription = read["classDescription"].ToString();
                    classFees = Convert.ToDouble(read["classFees"]);



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

        public static bool isLicenseClassExist(int licenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            //bu sorgunun soncu: eğer kayıt varsa bir sütun oluşur adı found ve sütun tek satırlı olur (çünkü her ID bir adet olduğu için) satırda 1 yazar. Bu demek oluyor ki bu ID var.

            string query = "select found =1 from LicenseClasses where licenseClassID=@licenseClassID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@licenseClassID", licenseClassID);


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
