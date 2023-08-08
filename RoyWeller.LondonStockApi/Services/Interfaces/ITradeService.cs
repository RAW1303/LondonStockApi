using RoyWeller.LondonStockApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services.Interfaces;
public interface ITradeService
{
    Task CreateTradesAsync(IEnumerable<TradeDto> trade);
}
