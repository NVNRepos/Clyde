using System;
using System.Windows.Input;

namespace Clyde.App.Commands
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        #endregion

        #region ctor

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            => (_execute, _canExecute) = (execute ?? throw new ArgumentNullException(nameof(execute)), canExecute);

        public RelayCommand(Action execute) : this(param => execute()) { }

        #endregion

        #region EventHandler

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion

        #region Methods

        public bool CanExecute(object parameter)
            => _canExecute is null || _canExecute(parameter);

        public void Execute(object parameter)
            => _execute(parameter);

        #endregion Methods
    }
}
