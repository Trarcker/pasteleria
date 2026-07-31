using BakeryPOS.Core.Models;

namespace BakeryPOS.Core.Services;

public interface ISalesService
{
    bool ValidatePayment(decimal totalAmount, decimal cashPaid, out decimal change);
    Task<Sale> ProcessSaleAsync(int userId, IEnumerable<SaleDetail> items, decimal cashPaid);
}