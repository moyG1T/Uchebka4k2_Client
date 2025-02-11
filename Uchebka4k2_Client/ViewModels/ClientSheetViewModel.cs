using System;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;
using Uchebka4k2_Client.Data.Models;
using System.Windows.Input;
using Uchebka4k2_Client.Domain.Commands;
using Uchebka4k2_Client.Domain.Contexts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.IO;
using System.Security.Policy;
using System.Windows.Interop;
using System.Windows;

namespace Uchebka4k2_Client.ViewModels
{
    public class ClientSheetViewModel : ViewModel
    {
        private readonly ClientContext _clientContext;
        private readonly AutoServiceEntities _db;

        public Client Client { get; } = new Client();
        public List<Gender> Genders { get; } = new List<Gender>();
        public Gender SelectedGender { get; set; }

        public ICommand GoBackCommand { get; }
        public ICommand PickImageCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public ClientSheetViewModel(INavService back, ClientContext clientContext, AutoServiceEntities db)
        {
            GoBackCommand = new BackCommand(back);
            PickImageCommand = new RelayCommand(OpenFileDialog);
            SaveChangesCommand = new RelayCommand(SaveChanges);

            if (clientContext.Client != null)
            {
                var client = clientContext.Client;

                Client = new Client
                {
                    ID = client.ID,
                    Birthday = client.Birthday,
                    Email = client.Email,
                    Phone = client.Phone,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Patronymic = client.Patronymic,
                    PhotoBin = client.PhotoBin,
                    PhotoPath = client.PhotoPath,
                    RegistrationDate = client.RegistrationDate,
                    GenderCode = client.GenderCode,
                };

                SelectedGender = client.Gender;
            }

            Genders = db.Genders.AsNoTracking().ToList();

            _clientContext = clientContext;
            _db = db;
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
                Client.PhotoBin = bytes;
                Client.PhotoPath = Path.GetFileName(fileDialog.FileName);
            }
        }

        private void SaveChanges()
        {
            if (Client.ID == 0)
            {
                try
                {
                    var newClient = new Client
                    {
                        GenderCode = SelectedGender.Code,

                        FirstName = Client.FirstName,
                        LastName = Client.LastName,
                        Patronymic = Client.Patronymic,
                        Birthday = Client.Birthday,
                        Email = Client.Email,
                        Phone = Client.Phone,
                        PhotoBin = Client.PhotoBin,
                        PhotoPath = Client.PhotoPath,
                        RegistrationDate = DateTime.Now,
                    };

                    _db.Clients.Add(newClient);
                    _db.SaveChanges();

                    _clientContext.AddClientNotify(newClient);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                var c = _clientContext.Client;

                c.GenderCode = SelectedGender.Code;
                c.FirstName = Client.FirstName;
                c.LastName = Client.LastName;
                c.Patronymic = Client.Patronymic;
                c.Birthday = Client.Birthday;
                c.Email = Client.Email;
                c.Phone = Client.Phone;
                c.PhotoBin = Client.PhotoBin;
                c.PhotoPath = Client.PhotoPath;

                _db.SaveChanges();
            }

            GoBackCommand.Execute(null);
        }

        public override void Dispose()
        {
            _clientContext.Client = null;

            GC.SuppressFinalize(this);
        }
    }
}
