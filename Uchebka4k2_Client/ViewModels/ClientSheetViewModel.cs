using System;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;
using Uchebka4k2_Client.Data.Models;
using System.Windows.Input;
using Uchebka4k2_Client.Domain.Commands;

namespace Uchebka4k2_Client.ViewModels
{
    public class ClientSheetViewModel : ViewModel
    {
        public ICommand GoBackCommand { get; }

        public ClientSheetViewModel(INavService back, AutoServiceEntities db)
        {
            GoBackCommand = new BackCommand(back);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
