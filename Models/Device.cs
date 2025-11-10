using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WPF_EquipmentMonitor.Models
{
    public enum DeviceStatus
    {
        [Display(Name = "В работе")]
        Working,
        [Display(Name = "Сломан")]
        Broken,
        [Display(Name = "Списан")]
        Decommissioned
    }

    public class Device : INotifyPropertyChanged
    {
        private string _category = "Без категории";
        public string Category
        {
            get => _category;
            set { if (_category != value) { _category = value; OnPropertyChanged(nameof(Category)); } }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get => _serialNumber;
            set { if (_serialNumber != value) { _serialNumber = value; OnPropertyChanged(nameof(SerialNumber)); } }
        }

        private DateTime _installationDate = DateTime.Now;
        public DateTime InstallationDate
        {
            get => _installationDate;
            set { if (_installationDate != value) { _installationDate = value; OnPropertyChanged(nameof(InstallationDate)); } }
        }

        private DeviceStatus _status = DeviceStatus.Working;
        public DeviceStatus Status
        {
            get => _status;
            set { if (_status != value) { _status = value; OnPropertyChanged(nameof(Status)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
