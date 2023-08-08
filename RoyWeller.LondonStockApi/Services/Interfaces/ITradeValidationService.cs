using RoyWeller.LondonStockApi.Model;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services.Interfaces;

public interface ITradeValidationService
{
    Task ValidateTradeAsync(TradeDto trade);
}