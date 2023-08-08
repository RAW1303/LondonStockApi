using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoyWeller.LondonStockApi.Translators;
internal static class StockTranslator
{
    public static StockDto TranslateToDto(Stock stock)
    {
        return new StockDto
        {
            Ticker = stock.Ticker,
            Price = stock.Trades.Any() ? Queryable.Average(stock.Trades.Select(x => x.Price).AsQueryable()) : 0
        };
    }

    public static List<StockDto> TranslateToDto(IEnumerable<Stock> stocks)
    {
        return stocks.Select(stock => TranslateToDto(stock)).ToList();
    }
}
