using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Uchebka4k2_Client.Data.Models;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class ClientsViewModel : ViewModel
    {
        private readonly AutoServiceEntities _db;
        public ObservableCollection<Client> Clients { get; set; }

        public ICommand EditClientCommand { get; }
        public ICommand RemoveClientCommand { get; }

        public ClientsViewModel(INavService clientSheet, AutoServiceEntities db)
        {
            _db = db;

            Clients = new ObservableCollection<Client>(_db.Clients.Where(it => !it.IsDeleted).ToList());

            RemoveClientCommand = new RelayCommand(RemoveClient);
            EditClientCommand = new GoCommand(clientSheet);
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

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
