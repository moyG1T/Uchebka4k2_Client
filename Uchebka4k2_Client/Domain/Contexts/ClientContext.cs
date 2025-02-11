using System;
using Uchebka4k2_Client.Data.Models;

namespace Uchebka4k2_Client.Domain.Contexts
{
    public class ClientContext
    {
        public Client Client { get; set; }
        public event Action<Client> ClientAdded;
        public void AddClientNotify(Client client)
        {
            ClientAdded?.Invoke(client);
        }
    }
}
