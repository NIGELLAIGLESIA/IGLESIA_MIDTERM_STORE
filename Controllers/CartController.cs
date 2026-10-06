using Microsoft.AspNetCore.Mvc;
using IGLESIA_MIDTERM_STORE.Data;

namespace IGLESIA_MIDTERM_STORE.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();

            return View(cartItems);
        }

        public IActionResult Remove(int id)
        {
            var cartItem = _db.CartItems.Find(id);

            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var cartItem = _db.CartItems.Find(id);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}