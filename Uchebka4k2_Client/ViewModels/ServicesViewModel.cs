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
    public class ServicesViewModel : ViewModel
    {
        private readonly INavService _navService;
        private readonly ServiceContext _context;
        private readonly AutoServiceEntities _db;
        public ObservableCollection<Service> Services { get; set; }

        public ICommand GoBackCommand { get; }
        public ICommand EditServiceCommand { get; }
        public ICommand RemoveServiceCommand { get; }
        public ICommand CreateServiceCommand { get; }


        public ServicesViewModel(INavService navService, ServiceContext context, AutoServiceEntities db)
        {
            _navService = navService;
            _context = context;
            _db = db;

            Services = new ObservableCollection<Service>(_db.Services.Where(it => !it.IsDeleted).ToList());

            _context.ServiceAdded += OnServiceAdded;

            GoBackCommand = new BackCommand(_navService);
            EditServiceCommand = new RelayCommand(EditService);
            RemoveServiceCommand = new RelayCommand(RemoveService);
            CreateServiceCommand = new GoCommand(_navService);
        }

        private void EditService(object param)
        {
            if (param is Service service)
            {
                _context.Service = service;
                _navService.Go();
            }
        }

        private void RemoveService(object param)
        {
            if (param is Service service)
            {
                service.IsDeleted = true;
                _db.SaveChanges();

                Services.Remove(service);
            }
        }

        private void OnServiceAdded(Service service)
        {
            Services.Add(service);
        }

        public override void Dispose()
        {
            _context.ServiceAdded -= OnServiceAdded;

            GC.SuppressFinalize(this);
        }
    }
}
