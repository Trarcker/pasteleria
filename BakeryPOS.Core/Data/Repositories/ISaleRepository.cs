using BakeryPOS.Core.Models;

namespace BakeryPOS.Core.Data.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime from, DateTime to);
    Task<Sale?> GetByIdAsync(int id);
}