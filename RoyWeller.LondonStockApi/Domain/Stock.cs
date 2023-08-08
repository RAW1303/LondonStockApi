using System.Collections.Generic;

namespace RoyWeller.LondonStockApi.Domain;
public class Stock : DomainEntity
{
    public string Ticker { get; set; }
    public virtual IEnumerable<Trade> Trades { get; set; }
}
