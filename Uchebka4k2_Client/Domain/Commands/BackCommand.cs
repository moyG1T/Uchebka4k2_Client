using System;
using System.Windows.Input;
using Uchebka4k2_Client.Domain.IServices;

namespace Uchebka4k2_Client.Domain.Commands
{
    class BackCommand : ICommand
    {
        private readonly INavService _navService;

        public BackCommand(INavService navService)
        {
            _navService = navService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _navService.Back();
        }
    }
}
