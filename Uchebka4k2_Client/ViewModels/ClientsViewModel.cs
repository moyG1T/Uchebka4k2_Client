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
    public class ClientsViewModel : ViewModel
    {
        private readonly INavService _clientSheet;
        private readonly ClientContext _clientContext;
        private readonly AutoServiceEntities _db;
        public ObservableCollection<Client> Clients { get; set; }

        public ICommand EditClientCommand { get; }
        public ICommand RemoveClientCommand { get; }
        public ICommand CreateClientCommand { get; }

        public ClientsViewModel(INavService clientSheet, ClientContext clientContext, AutoServiceEntities db)
        {
            _clientSheet = clientSheet;
            _clientContext = clientContext;
            _db = db;

            Clients = new ObservableCollection<Client>(_db.Clients.Where(it => !it.IsDeleted).ToList());

            RemoveClientCommand = new RelayCommand(RemoveClient);
            EditClientCommand = new RelayCommand(PushClient);
            CreateClientCommand = new GoCommand(clientSheet);

            _clientContext.ClientAdded += OnClientAdded;
        }

        private void PushClient(object param)
        {
            if (param is Client client)
            {
                _clientContext.Client = client;
                _clientSheet.Go();
            }
        }

        private void RemoveClient(object param)
        {
            if (param is Client client)
            {
                client.IsDeleted = true;
                _db.SaveChanges();

                Clients.Remove(client);
            }
        }

        private void OnClientAdded(Client client)
        {
            Clients.Add(client);
        }

        public override void Dispose()
        {
            _clientContext.ClientAdded -= OnClientAdded;

            GC.SuppressFinalize(this);
        }
    }
}
