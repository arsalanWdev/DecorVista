
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;
using DecorVista.DataAccess.Repository.IRepository;
using DecorVista.Models;
using DecorVista.Utility;

namespace DecorVista.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofwork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitofwork = unitOfWork;
        }

        public IActionResult Index(string searchName, double? minPrice, double? maxPrice, int? categoryId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart,
                   _unitofwork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
            }

            ViewBag.Categories = _unitofwork.Category.GetAll();

            IEnumerable<Product> productlist = _unitofwork.Product.GetAll(includeproperties: "Category");

            if (!string.IsNullOrEmpty(searchName))
            {
                productlist = productlist.Where(p => p.Title.Contains(searchName));
            }

            if (minPrice.HasValue)
            {
                productlist = productlist.Where(p => p.Price100 >= (double)minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productlist = productlist.Where(p => p.Price100 <= (double)maxPrice.Value);
            }

            if (categoryId.HasValue)
            {
                productlist = productlist.Where(p => p.CategoryId == categoryId.Value);
            }

            return View(productlist);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Detail(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitofwork.Product.Get(u => u.Id == productId, includeproperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Detail(ShoppingCart shoppingcart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingcart.ApplicationUserId = UserId;

            ShoppingCart cartfromdb = _unitofwork.ShoppingCart.Get(u => u.ApplicationUserId == UserId &&
            u.ProductId == shoppingcart.ProductId);

            if (cartfromdb != null)
            {
                cartfromdb.Count += shoppingcart.Count;
                _unitofwork.ShoppingCart.Update(cartfromdb);
                _unitofwork.Save();
            }
            else
            {
                _unitofwork.ShoppingCart.Add(shoppingcart);
                _unitofwork.Save();

                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitofwork.ShoppingCart.GetAll(u => u.ApplicationUserId == UserId).Count());
            }

            TempData["success"] = "Cart Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
