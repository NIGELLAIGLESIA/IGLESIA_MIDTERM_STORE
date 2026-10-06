using Microsoft.AspNetCore.Mvc;
using IGLESIA_MIDTERM_STORE.Data;
using IGLESIA_MIDTERM_STORE.Models;

namespace IGLESIA_MIDTERM_STORE.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db) { _db = db; }

        // shows the list
        public IActionResult Index(string searchString)
        {
            var products = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            ViewData["searchString"] = searchString;

            return View(products.ToList());
        }

        // shows the empty add-form
        public IActionResult Create()
        {
            return View();
        }

        // saves a new product
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT - show the edit form
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        // EDIT - save the changes
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - remove the product
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ADD TO CART
        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}