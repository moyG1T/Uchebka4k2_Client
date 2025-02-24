using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uchebka4k2_Client.Data.Models
{
    public partial class ClientService
    {
        public bool InProgress => DateTime.Now < StartTime.AddHours(Service.DurationInSeconds);
    }
}
