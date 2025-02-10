using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.Domain.Services
{
    public class MainNavService : INavService
    {
        private readonly MainContext _mainContext;
        private readonly ViewModel _viewModel;

        public MainNavService(MainContext mainContext, ViewModel viewModel = null)
        {
            _mainContext = mainContext;
            _viewModel = viewModel;
        }

        public void Back()
        {
            _mainContext.Pop();
        }

        public void ClearAndGo()
        {
            _mainContext.PopAndPush(_viewModel);
        }

        public void Go()
        {
            _mainContext.Push(_viewModel);
        }
    }
}
