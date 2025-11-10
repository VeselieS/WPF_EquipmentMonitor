using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_EquipmentMonitor.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Raise([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string prop = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            Raise(prop);
            return true;
        }
    }
}
