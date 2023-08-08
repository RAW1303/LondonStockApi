using Microsoft.EntityFrameworkCore;
using RoyWeller.LondonStockApi.Domain;

namespace RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
internal abstract class BaseRespository<T> where T : DomainEntity
{
    protected readonly DatabaseContext _databaseContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRespository(DatabaseContext dbContext)
    {
        _databaseContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }
}
