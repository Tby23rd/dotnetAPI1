using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Models;
using System.Threading.Tasks;

namespace WebApplication2.Areas.Identity.Pages
{
    public class StockDataModel : PageModel
    {
        private readonly AlphaVantageService _alphaVantageService;

        public StockDataModel(AlphaVantageService alphaVantageService)
        {
            _alphaVantageService = alphaVantageService;
        }

        public StockDataViewModel StockData { get; set; }

        public async Task OnGetAsync(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                StockData = await _alphaVantageService.FetchStockDataAsync(symbol);
            }
            else
            {
                // Handle when no symbol is provided (You can set default values or handle as required)
                StockData = new StockDataViewModel();
            }
        }
    }
}
