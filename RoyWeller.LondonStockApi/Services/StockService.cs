using Microsoft.Extensions.Logging;
using RoyWeller.LondonStockApi.Model;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using RoyWeller.LondonStockApi.Services.Interfaces;
using RoyWeller.LondonStockApi.Translators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services;
internal class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly ILogger<StockService> _logger;

    public StockService(IStockRepository stockRepository, ILogger<StockService> logger)
    {
        _stockRepository = stockRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<StockDto>> GetStocksAsync()
    {
        try 
        { 
            var stocks = await _stockRepository.GetStocksAsync();
            return StockTranslator.TranslateToDto(stocks);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<StockDto>> GetStocksAsync(IEnumerable<string> tickers)
    {
        try
        {
            var stocks = await _stockRepository.GetStocksAsync(tickers);
            return StockTranslator.TranslateToDto(stocks);

        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
