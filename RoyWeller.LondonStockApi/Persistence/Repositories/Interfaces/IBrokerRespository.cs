using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
internal interface IBrokerRespository
{
    Task<bool> BrokerExists(int brokerId);
}
