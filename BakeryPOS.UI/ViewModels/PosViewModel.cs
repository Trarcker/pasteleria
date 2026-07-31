using System.Collections.ObjectModel;
using BakeryPOS.Core.Models;
using BakeryPOS.Core.Services;
using BakeryPOS.Core.Services.Hardware;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BakeryPOS.UI.ViewModels;

public partial class PosViewModel : ObservableObject
{
    private readonly ISalesService _salesService;
    private readonly IScaleService _scaleService;

    [ObservableProperty]
    private decimal _totalAmount;

    [ObservableProperty]
    private decimal _cashPaid;

    [ObservableProperty]
    private decimal _changeGiven;

    public ObservableCollection<SaleDetail> CartItems { get; } = new();

    public PosViewModel(ISalesService salesService, IScaleService scaleService)
    {
        _salesService = salesService;
        _scaleService = scaleService;
    }

    [RelayCommand]
    private void AddProduct(Product product)
    {
        decimal qty = 1.0m;

        // Si es producto por peso, se lee automáticamente de la báscula
        if (product.SalesUnit == UnitType.Kilogram)
        {
            qty = _scaleService.ReadWeight();
            if (qty <= 0) qty = 1.0m; // Fallback manual
        }

        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += qty;
        }
        else
        {
            CartItems.Add(new SaleDetail
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = qty
            });
        }

        UpdateTotal();
    }

    [RelayCommand]
    private async Task ProcessPaymentAsync()
    {
        if (!CartItems.Any()) return;

        if (_salesService.ValidatePayment(TotalAmount, CashPaid, out decimal change))
        {
            ChangeGiven = change;
            // Usuario 1 hardcodeado para ejemplo del turno actual
            await _salesService.ProcessSaleAsync(1, CartItems, CashPaid);
            
            CartItems.Clear();
            TotalAmount = 0;
            CashPaid = 0;
        }
    }

    private void UpdateTotal()
    {
        TotalAmount = CartItems.Sum(i => i.Subtotal);
    }
}