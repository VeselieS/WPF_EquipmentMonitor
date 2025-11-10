using System.Windows;
using System.Windows.Input;
using WPF_EquipmentMonitor.Helpers;
using WPF_EquipmentMonitor.Models;

namespace WPF_EquipmentMonitor.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public DeviceStatus Status { get; set; } = DeviceStatus.Working;

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public AddDeviceViewModel()
        {
            OkCommand = new RelayCommand(o =>
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    MessageBox.Show("Укажите имя устройства");
                    return;
                }

                CloseWindow(true);
            });

            CancelCommand = new RelayCommand(o => CloseWindow(false));
        }

        private void CloseWindow(bool result)
        {
            if (Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = result;
                window.Close();
            }
        }

        public Device CreateDevice()
        {
            return new Device
            {
                Category = this.Category,
                Name = this.Name,
                SerialNumber = this.SerialNumber,
                InstallationDate = this.InstallationDate,
                Status = this.Status
            };
        }
    }
}
