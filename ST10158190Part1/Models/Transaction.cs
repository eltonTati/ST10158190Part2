using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ST10158190Part1.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int Id { get; set; } // User ID
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TransactionDate { get; set; }

        public static int PlaceOrder(Transaction transaction)
        {
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "INSERT INTO [Transaction] (Id, ProductId, Quantity, TotalPrice, TransactionDate) VALUES (@Id, @ProductId, @Quantity, @TotalPrice, @TransactionDate)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", transaction.Id);
                cmd.Parameters.AddWithValue("@ProductId", transaction.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", transaction.TotalPrice);
                cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static List<Transaction> GetUserOrders(int userId)
        {
            List<Transaction> orders = new List<Transaction>();
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM [Transaction] WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Transaction order = new Transaction
                    {
                        TransactionId = Convert.ToInt32(reader["TransactionId"]),
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                        TransactionDate = Convert.ToDateTime(reader["TransactionDate"])
                    };
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }

        public static void UpdateOrder(Transaction transaction)
        {
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "UPDATE [Transaction] SET Quantity = @Quantity, TotalPrice = @TotalPrice WHERE TransactionId = @TransactionId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", transaction.TotalPrice);
                cmd.Parameters.AddWithValue("@TransactionId", transaction.TransactionId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
