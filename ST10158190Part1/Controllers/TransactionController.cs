using Microsoft.AspNetCore.Mvc;
using ST10158190Part1.Models;
using System;
using System.Data.SqlClient;

namespace ST10158190Part1.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public IActionResult PlaceOrder(int Id, int productId, int quantity)
        {
            if (!UserExists(Id))
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return RedirectToAction("About", new { id = Id });
            }

            var userOrders = Transaction.GetUserOrders(Id);
            Transaction existingTransaction = userOrders.Find(p => p.ProductId == productId);

            if (existingTransaction != null)
            {
                // Update existing order quantity and total price
                existingTransaction.Quantity += quantity;
                existingTransaction.TotalPrice = existingTransaction.Quantity * GetProductPrice(productId);
                Transaction.UpdateOrder(existingTransaction);
            }
            else
            {
                // Create a new order
                Transaction newTransaction = new Transaction
                {
                    Id = Id,
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = quantity * GetProductPrice(productId),
                    TransactionDate = DateTime.Now
                };
                Transaction.PlaceOrder(newTransaction);
            }

            return RedirectToAction("About", new { id = Id });
        }

        [HttpGet]
        public IActionResult About(int id)
        {
            var userOrders = Transaction.GetUserOrders(id);
            ViewData["userID"] = id;
            return View(userOrders);
        }

        private bool UserExists(int userId)
        {
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT COUNT(1) FROM dbo.userTable WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private decimal GetProductPrice(int productId)
        {
            string conString = "Server=tcp:cdlv-sql-server.database.windows.net,1433;Initial Catalog=cldv-sql-DB;Persist Security Info=False;User ID=eltonTati;Password=Heltontatysam123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT Price FROM Product WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0m;
            }
        }
    }
}
