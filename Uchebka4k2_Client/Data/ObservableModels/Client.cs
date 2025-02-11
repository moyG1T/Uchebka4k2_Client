using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Uchebka4k2_Client.Data.Models
{
    public partial class Client : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string patronymic;
        private DateTime? birthday;
        private DateTime registrationDate;
        private string email;
        private string phone;
        private byte[] photoBin;

        public string FirstName
        {
            get => firstName; set
            {
                firstName = value; OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => lastName; set
            {
                lastName = value; OnPropertyChanged();
            }
        }
        public string Patronymic
        {
            get => patronymic; set
            {
                patronymic = value; OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get => birthday; set
            {
                birthday = value; OnPropertyChanged();
            }
        }
        public DateTime RegistrationDate
        {
            get => registrationDate; set
            {
                registrationDate = value; OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email; set
            {
                email = value; OnPropertyChanged();
            }
        }
        public string Phone
        {
            get => phone; set
            {
                phone = value; OnPropertyChanged();
            }
        }
        public byte[] PhotoBin
        {
            get => photoBin; set
            {
                photoBin = value; OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
