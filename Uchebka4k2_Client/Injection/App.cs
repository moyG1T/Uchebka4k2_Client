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
            services.AddSingleton<ClientContext>();
            services.AddSingleton<ServiceContext>();
            services.AddSingleton<ClientServicesContext>();

            services.AddSingleton(MainWindowFactory);
            services.AddSingleton(MainViewModelFactory);

            services.AddTransient(NavMenuViewModelFactory);

            services.AddTransient(ClientsViewModelFactory);
            services.AddTransient(ClientSheetViewModelFactory);

            services.AddTransient(ServicesViewModelFactory);
            services.AddTransient(ServiceSheetViewModelFactory);

            services.AddTransient(ClientServicesViewModelFactory);
            services.AddTransient(ClientServiceSheetViewModelFactory);

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
            return new MainViewModel(NavMenuMainNavService(p), p.GetRequiredService<MainContext>());
        }
        private NavMenuViewModel NavMenuViewModelFactory(IServiceProvider p)
        {
            return new NavMenuViewModel(ClientsMainNavService(p), ServicesMainNavService(p), ClientServicesFactoryMainNavService(p));
        }
        private ClientsViewModel ClientsViewModelFactory(IServiceProvider p)
        {
            return new ClientsViewModel(
                ClientSheetMainNavService(p),
                p.GetRequiredService<ClientContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }
        private ClientSheetViewModel ClientSheetViewModelFactory(IServiceProvider p)
        {
            return new ClientSheetViewModel(
                BackOnlyMainNavService(p),
                p.GetRequiredService<ClientContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }
        private ServicesViewModel ServicesViewModelFactory(IServiceProvider p)
        {
            return new ServicesViewModel(
                ServiceSheetMainNavService(p),
                p.GetRequiredService<ServiceContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }
        private ServiceSheetViewModel ServiceSheetViewModelFactory(IServiceProvider p)
        {
            return new ServiceSheetViewModel(
                BackOnlyMainNavService(p),
                p.GetRequiredService<ServiceContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }
        private ClientServicesViewModel ClientServicesViewModelFactory(IServiceProvider p)
        {
            return new ClientServicesViewModel(
                ClientServiceSheetFactoryMainNavService(p),
                p.GetRequiredService<ClientServicesContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }
        private ClientServiceSheetViewModel ClientServiceSheetViewModelFactory(IServiceProvider p)
        {
            return new ClientServiceSheetViewModel(
                BackOnlyMainNavService(p),
                p.GetRequiredService<ClientServicesContext>(),
                p.GetRequiredService<AutoServiceEntities>()
                );
        }

        private MainNavService BackOnlyMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>());
        }
        private MainNavService NavMenuMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<NavMenuViewModel>);
        }
        private MainNavService ClientsMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientsViewModel>);
        }
        private MainNavService ClientSheetMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientSheetViewModel>);
        }
        private MainNavService ServicesMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ServicesViewModel>);
        }
        private MainNavService ServiceSheetMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ServiceSheetViewModel>);
        }
        private MainNavService ClientServicesFactoryMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientServicesViewModel>);
        }
        private MainNavService ClientServiceSheetFactoryMainNavService(IServiceProvider p)
        {
            return new MainNavService(p.GetRequiredService<MainContext>(), p.GetRequiredService<ClientServiceSheetViewModel>);
        }
    }
}
