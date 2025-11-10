using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF_EquipmentMonitor.Helpers;
using WPF_EquipmentMonitor.Models;


namespace WPF_EquipmentMonitor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static readonly string DataFile =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\devices.json"));

        public ObservableCollection<Device> Devices { get; } = new();
        public ICollectionView DevicesView { get; }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get => _selectedDevice;
            set => Set(ref _selectedDevice, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { if (Set(ref _searchText, value)) DevicesView.Refresh(); }
        }

        private DeviceStatus? _statusFilter;
        public DeviceStatus? StatusFilter
        {
            get => _statusFilter;
            set { if (Set(ref _statusFilter, value)) DevicesView.Refresh(); }
        }

        // Commands
        public ICommand AddDeviceCommand { get; }
        public ICommand DeleteDeviceCommand { get; }
        public ICommand SaveDeviceCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public MainWindowViewModel()
        {
            LoadData();

            DevicesView = CollectionViewSource.GetDefaultView(Devices);
            DevicesView.Filter = FilterDevice;
            DevicesView.SortDescriptions.Add(new SortDescription(nameof(Device.Category), ListSortDirection.Ascending));
            DevicesView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Device.Category)));

            AddDeviceCommand = new RelayCommand(_ => OnAddDevice());
            DeleteDeviceCommand = new RelayCommand(_ => OnDeleteDevice(), _ => SelectedDevice != null);
            SaveDeviceCommand = new RelayCommand(_ => SaveData(), _ => SelectedDevice != null);
            ClearFilterCommand = new RelayCommand(_ => { SearchText = ""; StatusFilter = null; });

            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedDevice))
                    ((RelayCommand)DeleteDeviceCommand).RaiseCanExecuteChanged();
            };
        }

        private bool FilterDevice(object obj)
        {
            if (obj is not Device d) return false;
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (!(d.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                    return false;
            }
            if (StatusFilter.HasValue && d.Status != StatusFilter.Value) return false;
            return true;
        }

        private void OnAddDevice()
        {
            // Заглушка, окно добавления открывается в code-behind
        }

        public void AddDevice(Device device)
        {
            Devices.Add(device);
            DevicesView.Refresh();
            SelectedDevice = device;
        }

        private void OnDeleteDevice()
        {
            if (SelectedDevice == null) return;
            var res = MessageBox.Show($"Удалить '{SelectedDevice.Name}'?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                Devices.Remove(SelectedDevice);
                SelectedDevice = null;
            }
        }

        public void LoadData()
        {
            try
            {
                if (!File.Exists(DataFile)) return;
                var json = File.ReadAllText(DataFile);
                var list = JsonSerializer.Deserialize<Device[]>(json);
                Devices.Clear();
                foreach (var d in list) Devices.Add(d);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении: {ex.Message}");
            }
        }

        public void SaveData()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataFile)!);
                var json = JsonSerializer.Serialize(Devices.ToArray(), new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(DataFile, json);
                Debug.WriteLine($"[SaveData] Сохранено {Devices.Count} устройств в {DataFile}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SaveData] Ошибка при сохранении: {ex}");
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}