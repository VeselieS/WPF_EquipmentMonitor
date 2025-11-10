using System.Windows.Input;

namespace WPF_EquipmentMonitor.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public RelayCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            _execute = exec ?? throw new ArgumentNullException(nameof(exec));
            _canExecute = canExec;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
