using BakeryPOS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryPOS.Core.Data.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly BakeryDbContext _context;

    public SaleRepository(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Sales.Add(sale);

            // Descuento automático de stock
            foreach (var detail in sale.Items)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= detail.Quantity;
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return sale;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime from, DateTime to)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.Date >= from && s.Date <= to)
            .ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}