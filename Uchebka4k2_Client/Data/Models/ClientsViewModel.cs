using System;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.Data.Models
{
    public class ClientsViewModel : ViewModel
    {
        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
