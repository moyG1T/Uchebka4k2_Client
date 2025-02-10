using Microsoft.Extensions.DependencyInjection;
using System;
using Uchebka4k2_Client.Data.Models;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.Services;
using Uchebka4k2_Client.ViewModels;

namespace Uchebka4k2_Client
{
    public partial class App
    {
        private readonly IServiceProvider _provider;

        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<AutoServiceEntities>();
            services.AddSingleton<MainContext>();

            services.AddSingleton(MainWindowFactory);
            services.AddSingleton(MainViewModelFactory);

            services.AddTransient(ClientsViewModelFactory);
            services.AddTransient(ClientSheetViewModelFactory);

            _provider = services.BuildServiceProvider();
        }

        private MainWindow MainWindowFactory(IServiceProvider p)
        {
            return new MainWindow
            {
                DataContext = p.GetRequiredService<MainViewModel>()
            };
        }
        private MainViewModel MainViewModelFactory(IServiceProvider p)
        {
            return new MainViewModel(ClientsMainNavService(p), p.GetRequiredService<MainContext>());
        }
        private ClientsViewModel ClientsViewModelFactory(IServiceProvider p)
        {
            return new ClientsViewModel(ClientSheetMainNavService(p), p.GetRequiredService<AutoServiceEntities>());
        }
        private ClientSheetViewModel ClientSheetViewModelFactory(IServiceProvider p)
        {
            return new ClientSheetViewModel(BackOnlyMainNavService(p), p.GetRequiredService<AutoServiceEntities>());
        }

        private MainNavService BackOnlyMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>());
        }
        private MainNavService ClientsMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientsViewModel>());
        }
        private MainNavService ClientSheetMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientSheetViewModel>());
        }
    }
}
