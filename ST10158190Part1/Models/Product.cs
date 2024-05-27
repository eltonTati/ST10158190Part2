using System;
using System.Data.SqlClient;

namespace ST10158190Part1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProdName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }

        private static string con_string = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        private static SqlConnection con = new SqlConnection(con_string);

        public static int InsertProduct(Product product)
        {
            try
            {
                string sql = "INSERT INTO Product (ProdName, ProductDescription, ProductPrice) VALUES (@ProdName, @ProductDescription, @ProductPrice)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProdName", product.ProdName);
                cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);

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
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM Product";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        ProdName = reader["ProdName"].ToString(),
                        ProductDescription = reader["ProductDescription"].ToString(),
                        ProductPrice = (decimal)reader["ProductPrice"]
                       
                    });
                }
            }
            return products;
        }

        internal static int GetProductPrice(int productId)
        {
            throw new NotImplementedException();
        }
    }
}

