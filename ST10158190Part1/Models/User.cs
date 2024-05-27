using System;
using System.Data.SqlClient;

namespace ST10158190Part1.Models
{
    public class User
    {
         public static string con_string = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
       public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int insert_User(User m)
        {
            try
            {
                string sql = "INSERT INTO [userTable] (Name, Password, Email) VALUES (@Name, @Password, @Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Password", m.Password);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }
        }
    }
}

