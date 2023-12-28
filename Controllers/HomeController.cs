using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AlphaVantageService _alphaVantageService;

        public HomeController(ILogger<HomeController> logger, AlphaVantageService alphaVantageService)
        {
            _logger = logger;
            _alphaVantageService = alphaVantageService;
        }

        // Display the main index page
        public IActionResult Index()
        {
            return View(); // Assuming you have an Index view that accepts a model of type StockDataViewModel
        }

        // Handle form submissions from the main index page
        [HttpPost]
        public async Task<IActionResult> Index(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return RedirectToAction("Index");
            }

            // Fetch stock data for the specified symbol using the AlphaVantageService
            var stockData = await _alphaVantageService.FetchStockDataAsync(symbol);

            // Create an instance of StockDataViewModel and populate it with fetched stock data
            var viewModel = new StockDataViewModel
            {
                // Populate the properties of StockDataViewModel based on the fetched stock data
                // For example:
                Date = stockData.Date,
                Open = stockData.Open,
                High = stockData.High,
                Low = stockData.Low,
                Close = stockData.Close,
                Volume = stockData.Volume
            };

            return View(viewModel); // Return the view with the populated StockDataViewModel instance
        }

        // Other actions remain unchanged
        // ...
    }
}
