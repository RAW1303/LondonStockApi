using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RoyWeller.LondonStockApi.Services.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace RoyWeller.LondonStockApi.Functions;
public class StockFunctions
{
    private readonly IStockService _stockService;
    private readonly ILogger _logger;

    public StockFunctions(
        IStockService stockService,
        ILogger<StockFunctions> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    [FunctionName(nameof(GetStocks))]
    public async Task<IActionResult> GetStocks(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stocks")] HttpRequest req)
    {
        var stocks = await _stockService.GetStocksAsync();
        return new OkObjectResult(stocks);
    }

    [FunctionName(nameof(GetStock))]
    public async Task<IActionResult> GetStock(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stocks/{ticker}")] HttpRequest req,
        string ticker)
    {
        var stocks = await _stockService.GetStocksAsync(new[] { ticker });
        return stocks.Any() ? new OkObjectResult(stocks.First()) : new NotFoundResult();
    }

    [FunctionName(nameof(QueryStocks))]
    public async Task<IActionResult> QueryStocks(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "stocks/query")] HttpRequest req)
    {
        var requestContentString = await req.ReadAsStringAsync();
        var tickers = JsonConvert.DeserializeObject<string[]>(requestContentString);
        var stocks = await _stockService.GetStocksAsync(tickers);
        return stocks.Any() ? new OkObjectResult(stocks) : new NotFoundResult();
    }
}
