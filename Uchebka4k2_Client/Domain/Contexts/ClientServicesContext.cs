using System;
using Uchebka4k2_Client.Data.Models;

namespace Uchebka4k2_Client.Domain.Contexts
{
    public class ClientServicesContext
    {
        public event Action<ClientService> ClientServiceAdded;
        public void AddClientServicesNotify(ClientService clientService)
        {
            ClientServiceAdded?.Invoke(clientService);
        }
    }
}
