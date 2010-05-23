using System;
using System.Windows.Input;

namespace Driverslog.Commands {
    public class DelegateCommand : ICommand {
        
        private readonly Action<object>     _action;
        private readonly Func<object, bool> _canExecute;
        private bool                        _canExecuteCache;

        public DelegateCommand(Action<object> action)
            : this(action, (o) => true) {
        }

        public DelegateCommand(Action<object> action, Func<object, bool> canExecute) {
            _action     = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            var canExecute = _canExecute(parameter);
            if(ShouldFireCanExecuteChanged(canExecute)) {
                CacheCanExecute(canExecute);
                FireCanExecuteChanged();
            }
            return _canExecuteCache;
        }

        private bool ShouldFireCanExecuteChanged(bool canExecute) {
            return canExecute != _canExecuteCache;
        }

        private void CacheCanExecute(bool canExecute) {
            _canExecuteCache = canExecute;
        }

        private void FireCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter) {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}