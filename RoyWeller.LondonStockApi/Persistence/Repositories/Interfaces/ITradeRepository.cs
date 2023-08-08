using RoyWeller.LondonStockApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
internal interface ITradeRepository
{
    Task CreateTradesAsync(IEnumerable<Trade> tasks);
}
