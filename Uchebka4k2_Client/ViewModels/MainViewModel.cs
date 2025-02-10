using System;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Services;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly MainContext _mainContext;
        private MainNavService mainNavService;

        public ViewModel CurrentViewModel => _mainContext.ViewModel;

        public MainViewModel(INavService clients, MainContext mainContext)
        {
            clients.Go();

            _mainContext = mainContext;
            _mainContext.ViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _mainContext.ViewModelChanged -= OnViewModelChanged;

            GC.SuppressFinalize(this);
        }
    }
}
