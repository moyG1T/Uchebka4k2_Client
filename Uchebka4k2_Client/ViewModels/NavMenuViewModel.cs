using System;
using System.Windows.Input;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class NavMenuViewModel : ViewModel
    {
        public ICommand PushClientsCommand { get; }
        public ICommand PushServicesCommand { get; }
        public ICommand PushClientServicesCommand { get; }

        public NavMenuViewModel(INavService clients, INavService services, INavService clientServices)
        {
            PushClientsCommand = new GoCommand(clients);
            PushServicesCommand = new GoCommand(services);
            PushClientServicesCommand = new GoCommand(clientServices);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
