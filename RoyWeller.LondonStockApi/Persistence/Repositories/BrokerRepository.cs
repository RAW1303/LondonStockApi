using Microsoft.EntityFrameworkCore;
using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories;
internal class BrokerRepository : BaseRespository<Broker>, IBrokerRespository
{
    public BrokerRepository(DatabaseContext databaseContext) : base(databaseContext) { }

    public async Task<bool> BrokerExists(int brokerId)
    {
        return await _dbSet.AnyAsync(b => b.Id == brokerId);
    }
}
