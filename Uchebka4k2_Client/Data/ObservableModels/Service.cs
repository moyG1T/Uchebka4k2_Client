using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Uchebka4k2_Client.Data.Models
{
    public partial class Service : INotifyPropertyChanged
    {
        private string title;
        private decimal cost;
        private int durationInSeconds;
        private string description;
        private double? discount;
        private string mainImagePath;
        private byte[] mainImageBin;
        private bool isDeleted;

        public int DiscountCost
        {
            get
            {
                double discount = Discount ?? 0;

                return (int)(discount == 0 ? Cost : Cost - (((decimal)discount / 100) * Cost));
            }
        }


        public string Title
        {
            get => title; set
            {
                title = value; OnPropertyChanged();
            }
        }
        public decimal Cost
        {
            get => cost; set
            {
                cost = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiscountCost));
            }
        }
        public int DurationInSeconds
        {
            get => durationInSeconds; set
            {
                durationInSeconds = value; OnPropertyChanged();
            }
        }
        public string Description
        {
            get => description; set
            {
                description = value; OnPropertyChanged();
            }
        }
        public Nullable<double> Discount
        {
            get => discount; set
            {
                discount = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiscountCost));
            }
        }
        public string MainImagePath
        {
            get => mainImagePath; set
            {
                mainImagePath = value; OnPropertyChanged();
            }
        }
        public byte[] MainImageBin
        {
            get => mainImageBin; set
            {
                mainImageBin = value; OnPropertyChanged();
            }
        }
        public bool IsDeleted
        {
            get => isDeleted; set
            {
                isDeleted = value; OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
