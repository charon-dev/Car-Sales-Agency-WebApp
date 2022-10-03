using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CarSalesAgency.Models;
using CarSalesAgency.DataAccess.Repository.IRepository;
using CarSalesAgency.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarSalesAgency.Utility;

namespace CarSalesAgencyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Car> carList = _unitOfWork.Car.GetAll(includeProperties: "Brand");
            return View(carList);
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
        public IActionResult Details(int CarId)
        {
            ShoppingCarts carObj = new()
            {
                Count = 1,
                CarId = CarId,
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == CarId, includeProperties: "Brand")
            };
            return View(carObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Enforce the user to log in to access the post access method
        [Authorize]
        public IActionResult Details(ShoppingCarts shoppingCart)
        {
            //the user object is always available by default
            var claimsIDentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            //Retrieve the existing entry and modify the count 
            ShoppingCarts cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.CarId == shoppingCart.CarId
                );
            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                //when we add a new shoppingcart session increment
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);

            }
            else
            {
                //Method to update count
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }


            return RedirectToAction(nameof(Index));
        }
    }
}