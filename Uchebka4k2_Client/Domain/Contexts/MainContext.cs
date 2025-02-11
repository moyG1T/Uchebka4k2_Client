using System;
using System.Collections.Generic;
using Uchebka4k2_Client.Domain.Utils;

namespace Uchebka4k2_Client.Domain.Contexts
{
    public class MainContext
    {
        private readonly Stack<ViewModel> _history = new Stack<ViewModel>();

        public ViewModel ViewModel => _history.Peek();

        public event Action ViewModelChanged;

        public void Pop()
        {
            if (_history.Count > 0)
            {
                var viewModel = _history.Pop();
                viewModel.Dispose();
                ViewModelChanged?.Invoke();
            }
        }

        public void Push(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            _history.Push(viewModel);
            ViewModelChanged?.Invoke();
        }

        public void PopAndPush(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            if (_history.Count > 0)
            {
                foreach (var item in _history)
                {
                    item.Dispose();
                }
                _history.Clear();
            }

            _history.Push(viewModel);
            ViewModelChanged?.Invoke();
        }
    }
}
