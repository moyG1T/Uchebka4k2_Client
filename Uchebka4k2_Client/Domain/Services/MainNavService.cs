using System;
using Uchebka4k2_Client.Domain.Contexts;
using Uchebka4k2_Client.Domain.IServices;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.Domain.Services
{
    public class MainNavService : INavService
    {
        private readonly MainContext _mainContext;
        private readonly Func<ViewModel> _viewModelFunc;

        public MainNavService(MainContext mainContext, Func<ViewModel> viewModelFunc = null)
        {
            _mainContext = mainContext;
            _viewModelFunc = viewModelFunc;
        }

        public void Back()
        {
            _mainContext.Pop();
        }

        public void ClearAndGo()
        {
            _mainContext.PopAndPush(_viewModelFunc());
        }

        public void Go()
        {
            _mainContext.Push(_viewModelFunc());
        }
    }
}
