using System;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class MainViewModel : ViewModel
    {

        public MainViewModel(INavService clients)
        {
            clients.Go();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
