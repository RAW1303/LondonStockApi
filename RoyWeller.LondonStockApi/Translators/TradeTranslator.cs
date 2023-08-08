using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace RoyWeller.LondonStockApi.Translators;
internal static class TradeTranslator
{
    public static Trade TranslateToDomain(TradeDto trade)
    {
        return new Trade
        {
            StockTicker = trade.StockTicker,
            BrokerId = trade.BrokerId,
            Price = trade.Price,
            Quantity = trade.Quantity
        };
    }

    public static List<Trade> TranslateToDomain(IEnumerable<TradeDto> trades)
    {
        return trades.Select(trades => TranslateToDomain(trades)).ToList();
    }
}
