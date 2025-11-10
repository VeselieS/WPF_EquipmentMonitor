using System.Windows;
using System.Windows.Input;
using WPF_EquipmentMonitor.Models;

namespace WPF_EquipmentMonitor.Views
{
    public partial class AddDeviceWindow : Window
    {
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public DeviceStatus Status { get; set; } = DeviceStatus.Working;

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public AddDeviceWindow()
        {
            InitializeComponent();
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
