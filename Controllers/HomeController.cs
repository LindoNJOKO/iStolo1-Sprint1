using iStolo1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iStolo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Main()
        {
            var model = new MainViewModel
            {
                Message = "Welcome to the Main Page"
            };

            return View(model);
        }

        public ActionResult ProductDetails()
        {
            var product = new Products
            {
                ProductId = 1, // Set the product ID as needed
                Price = 10, // Set the price as needed
                Title = "Sample Product", // Set the title as needed
                ProductDescription = "Sample Product Description" // Set the description as needed
            };

            return View("View", product);
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
