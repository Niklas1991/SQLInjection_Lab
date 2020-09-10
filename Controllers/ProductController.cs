using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SQLInjection_Lab1.Data;
using SQLInjection_Lab1.Models;

namespace SQLInjection_Lab1.Controllers
{
    public class ProductController : Controller
    {
        private readonly SQLInjection_Lab1Context _context;

        public ProductController(SQLInjection_Lab1Context context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string productName, int productPrice, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLlocaldb;Initial Catalog=SQLInjection_Lab1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
                
                
                //string unsafeQuery = @"INSERT INTO Products VALUES ('" + productName + "', " + productPrice + ", " + quantity + ")";
                string query = @"INSERT INTO Products (ProductName, ProductPrice, Quantity) VALUES(@ProductName, @ProductPrice, @Quantity)";
                SqlCommand command = new SqlCommand(query, connection);
                //Create the parameters objects as specific as possible
                //command.Parameters.Add("@ProductName", System.Data.SqlDbType.NVarChar, 30);
                //command.Parameters.Add("@ProductPrice", System.Data.SqlDbType.Int, 10);
                //command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int, 100);


                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);
                command.Parameters.AddWithValue("@Quantity", quantity);
                connection.Open();
                await command.ExecuteNonQueryAsync();



                return View();
            }
            
        }
        

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products product)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLlocaldb;Initial Catalog=SQLInjection_Lab1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {


                string unsafeQuery = @"UPDATE Products SET ProductName = '" + product.ProductName + "', ProductPrice = " + product.ProductPrice + ", Quantity = " + product.Quantity + " WHERE ID = " + id;
                // SQL INJECTION CODE EXAMPLE Test', ProductPrice=9, Quantity=1 WHERE ID=20 delete from Products;--
                SqlCommand command = new SqlCommand(unsafeQuery, connection);
                
                connection.Open();
                await command.ExecuteNonQueryAsync();



                return View();
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
