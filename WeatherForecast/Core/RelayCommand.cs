using System;
using System.Windows.Input;

namespace WeatherForecast.Core
{
    //класс для упрощения создания команд взаимодействия с пользовательским интерфейсом
    public class RelayCommand : IDelegateCommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteAction;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _executeAction = execute;
            _canExecuteAction = canExecute;
        }

        public RelayCommand(Action<object> execute)
        {
            _executeAction = execute;
            _canExecuteAction = this.AlwaysCanExecute;
        }

        public void Execute(object param) => _executeAction(param);

        public bool CanExecute(object param) => _canExecuteAction?.Invoke(param) ?? true;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private bool AlwaysCanExecute(object param)
        {
            return true;
        }
    }
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
