using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ST10158190Part1.Models
{
    public class ProductDisplayModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
       

        public ProductDisplayModel() { }

        public ProductDisplayModel(int id, string name, string description, decimal price)
        {
            ProductId = id;
            Name = name;
            Description = description;
            Price = price;
           
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT ProductId, Name, Description, Price FROM Product";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Description = Convert.ToString(reader["Description"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                       
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}
