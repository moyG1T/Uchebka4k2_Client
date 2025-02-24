using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Uchebka4k2_Client.Data.Models;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class ClientServiceSheetViewModel : ViewModel
    {
        private readonly ClientServicesContext _context;
        private readonly AutoServiceEntities _db;

        public Client SelectedClient { get; set; }
        public Service SelectedService { get; set; }

        public List<Client> Clients { get; set; }
        public List<Service> Services { get; set; }

        private string _comm;
        public string Comm
        {
            get => _comm;
            set { _comm = value; OnPropertyChanged(); }
        }


        public ICommand GoBackCommand { get; }
        public ICommand CreateOrderCommand { get; }

        public ClientServiceSheetViewModel(INavService back, ClientServicesContext context, AutoServiceEntities db)
        {
            _context = context;
            _db = db;

            GoBackCommand = new BackCommand(back);
            CreateOrderCommand = new RelayCommand(CreateOrder);

            Clients = _db.Clients.ToList();
            Services = _db.Services.ToList();
        }

        private void CreateOrder()
        {
            var newClientService = new ClientService
            {
                Client = SelectedClient,
                Service = SelectedService,
                Comment = Comm,
                StartTime = DateTime.Now,
            };

            _db.ClientServices.Add(newClientService);
            _db.SaveChanges();

            _context.AddClientServicesNotify(newClientService);

            GoBackCommand.Execute(null);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
