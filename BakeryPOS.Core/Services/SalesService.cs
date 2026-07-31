using BakeryPOS.Core.Data.Repositories;
using BakeryPOS.Core.Models;

namespace BakeryPOS.Core.Services;

public class SalesService : ISalesService
{
    private readonly ISaleRepository _saleRepository;

    public SalesService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public bool ValidatePayment(decimal totalAmount, decimal cashPaid, out decimal change)
    {
        change = 0;
        if (cashPaid < totalAmount) return false;
        
        change = cashPaid - totalAmount;
        return true;
    }

    public async Task<Sale> ProcessSaleAsync(int userId, IEnumerable<SaleDetail> items, decimal cashPaid)
    {
        var itemList = items.ToList();
        var total = itemList.Sum(i => i.Subtotal);

        if (!ValidatePayment(total, cashPaid, out decimal change))
        {
            throw new InvalidOperationException("Monto pagado insuficiente.");
        }

        var sale = new Sale
        {
            UserId = userId,
            Date = DateTime.Now,
            Total = total,
            CashPaid = cashPaid,
            ChangeGiven = change,
            Items = itemList
        };

        return await _saleRepository.CreateSaleAsync(sale);
    }
}