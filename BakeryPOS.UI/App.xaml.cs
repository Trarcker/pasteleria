using System.Windows;
using BakeryPOS.Core.Data;
using BakeryPOS.Core.Data.Repositories;
using BakeryPOS.Core.Services;
using BakeryPOS.Core.Services.Hardware;
using BakeryPOS.UI.ViewModels;
using BakeryPOS.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BakeryPOS.UI;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        Services = serviceCollection.BuildServiceProvider();

        // Crear/Asegurar la base de datos local SQLite al iniciar
        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
            db.Database.EnsureCreated();
        }

        var mainView = Services.GetRequiredService<PosView>();
        mainView.Show();

        base.OnStartup(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Infraestructura y Datos
        services.AddDbContext<BakeryDbContext>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        // Servicios
        services.AddScoped<ISalesService, SalesService>();
        services.AddSingleton<IScaleService, ScaleService>();

        // ViewModels
        services.AddTransient<PosViewModel>();

        // Vistas
        services.AddTransient<PosView>();
    }
}