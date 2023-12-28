using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2
{
    public class AlphaVantageService
    {
        private readonly string _apiKey;

        public AlphaVantageService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<StockDataViewModel> FetchStockDataAsync(string symbol)
        {
            string apiUrl = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                string jsonResponse = await client.GetStringAsync(apiUrl);
                // Deserialize JSON response to your structured model (assuming the JSON structure)
                var response = JsonSerializer.Deserialize<YourJsonResponseModel>(jsonResponse);

                // Convert your deserialized response to StockDataViewModel
                var stockDataViewModel = new StockDataViewModel
                {
                    Date = DateTime.Now,  // Replace with actual date from the response
                    Open = decimal.Parse(response.Open),  // Assuming 'Open' is a string in the response
                    High = decimal.Parse(response.High),  // Assuming 'High' is a string in the response
                    Low = decimal.Parse(response.Low),  // Assuming 'Low' is a string in the response
                    Close = decimal.Parse(response.Close),  // Assuming 'Close' is a string in the response
                    Volume = int.Parse(response.Volume)  // Assuming 'Volume' is a string in the response
                };

                return stockDataViewModel;
            }
        }
    }

    // Define a model to match the JSON structure (Replace with actual JSON structure)
    public class YourJsonResponseModel
    {
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }
    }
}
