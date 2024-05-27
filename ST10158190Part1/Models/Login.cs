using System.ComponentModel.DataAnnotations;
using System;
using System.Data.SqlClient;
namespace ST10158190Part1.Models
{

    public class Login
    {
        public static string con_string = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int SelectUser(string email, string name)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT Id FROM userTable WHERE Email = @Email AND Name = @Name";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Name", name);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return userId;
        }

    }
}

