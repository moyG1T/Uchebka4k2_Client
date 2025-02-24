using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Uchebka4k2_Client.Data.Models;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class ServiceSheetViewModel : ViewModel
    {
        private readonly ServiceContext _context;
        private readonly AutoServiceEntities _db;

        public Service Service { get; set; } = new Service();

        public ICommand GoBackCommand { get; }
        public ICommand PickImageCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public ServiceSheetViewModel(INavService back, ServiceContext context, AutoServiceEntities db)
        {
            _context = context;
            _db = db;

            GoBackCommand = new BackCommand(back);
            PickImageCommand = new RelayCommand(OpenFileDialog);
            SaveChangesCommand = new RelayCommand(SaveChanges);

            if (_context.Service != null)
            {
                var service = context.Service;

                Service = new Service
                {
                    ID = service.ID,
                    ClientServices = service.ClientServices,
                    Cost = service.Cost,
                    Description = service.Description,
                    Discount = service.Discount,
                    DurationInSeconds = service.DurationInSeconds,
                    IsDeleted = service.IsDeleted,
                    MainImageBin = service.MainImageBin,
                    MainImagePath = service.MainImagePath,
                    ServicePhotoes = service.ServicePhotoes,
                    Title = service.Title,
                };
            }
        }

        private void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Медиа|*.jpg;*.jpeg;*.png",
                Multiselect = false,
            };

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                var bytes = File.ReadAllBytes(fileDialog.FileName);
                Service.MainImageBin = bytes;
                Service.MainImagePath = Path.GetFileName(fileDialog.FileName);
            }
        }

        private void SaveChanges()
        {
            if (Service.Discount == null)
            {
                Service.Discount = 0;
            }
            if (Service.DurationInSeconds == 0)
            {
                MessageBox.Show("Ошибка времени услуги");
                return;
            }
            if (string.IsNullOrEmpty(Service.Title))
            {
                MessageBox.Show("Ошибка названия услуги");
                return;
            }

            if (Service.ID == 0)
            {
                try
                {
                    var newService = new Service
                    {
                        Title = Service.Title,
                        Description = Service.Description,
                        Cost = Service.Cost,
                        Discount = Service.Discount,
                        DurationInSeconds = Service.DurationInSeconds,
                        MainImageBin = Service.MainImageBin,
                        MainImagePath = Service.MainImagePath,
                    };

                    _db.Services.Add(newService);
                    _db.SaveChanges();

                    _context.AddServiceNotify(newService);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                try
                {
                    var c = _context.Service;

                    c.Title = Service.Title;
                    c.Description = Service.Description;
                    c.Discount = Service.Discount;
                    c.Cost = Service.Cost;
                    c.DurationInSeconds = Service.DurationInSeconds;
                    c.MainImageBin = Service.MainImageBin;
                    c.MainImagePath = Service.MainImagePath;

                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            GoBackCommand.Execute(null);
        }

        public override void Dispose()
        {
            _context.Service = null;

            GC.SuppressFinalize(this);
        }
    }
}
