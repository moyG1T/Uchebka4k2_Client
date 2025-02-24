using System;
using Uchebka4k2_Client.Data.Models;

namespace Uchebka4k2_Client.Domain.Contexts
{
    public class ServiceContext
    {
        public Service Service { get; set; }
        public event Action<Service> ServiceAdded;
        public void AddServiceNotify(Service service)
        {
            ServiceAdded?.Invoke(service);
        }
    }
}
