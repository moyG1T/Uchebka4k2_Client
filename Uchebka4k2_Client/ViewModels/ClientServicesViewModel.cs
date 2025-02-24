using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Uchebka4k2_Client.Data.Models;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    internal class ClientServicesViewModel : ViewModel
    {
        private readonly ClientServicesContext _context;
        private readonly AutoServiceEntities _db;

        public ICommand GoBackCommand { get; }
        public ICommand CreateClientServiceCommand { get; }

        public ObservableCollection<ClientService> ClientServices { get; set; }

        public ClientServicesViewModel(INavService navService, ClientServicesContext context, AutoServiceEntities db)
        {
            _context = context;
            _db = db;

            GoBackCommand = new BackCommand(navService);
            CreateClientServiceCommand = new GoCommand(navService);

            ClientServices = new ObservableCollection<ClientService>(_db.ClientServices.ToList());

            _context.ClientServiceAdded += OnClientServiceAdded;
        }

        private void OnClientServiceAdded(ClientService clientService)
        {
            ClientServices.Add(clientService);
        }

        public override void Dispose()
        {
            _context.ClientServiceAdded -= OnClientServiceAdded;

            GC.SuppressFinalize(this);
        }
    }
}
