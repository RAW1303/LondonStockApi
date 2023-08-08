using Microsoft.Extensions.Logging;
using RoyWeller.LondonStockApi.Model;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using RoyWeller.LondonStockApi.Services.Interfaces;
using RoyWeller.LondonStockApi.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services;
internal class TradeService : ITradeService
{
    private readonly IStockRepository _stockRepository;
    private readonly ITradeRepository _tradeRepository;
    private readonly ILogger<TradeService> _logger;

    public TradeService(
        IStockRepository stockRepository,
        ITradeRepository tradeRepository, 
        ILogger<TradeService> logger)
    {
        _stockRepository = stockRepository;
        _tradeRepository = tradeRepository;
        _logger = logger;
    }

    public async Task CreateTradesAsync(IEnumerable<TradeDto> trades)
    {
        try
        {
            var tradeStockTickers = trades.Select(t => t.StockTicker).Distinct();
            await _stockRepository.CreateMissingStocksAsync(tradeStockTickers);
            var domainTrades = TradeTranslator.TranslateToDomain(trades);
            await _tradeRepository.CreateTradesAsync(domainTrades);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
