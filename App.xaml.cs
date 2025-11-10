using System.Configuration;
using System.Data;
using System.Windows;
using WPF_EquipmentMonitor.ViewModels;

namespace WPF_EquipmentMonitor
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var vm = new MainWindowViewModel();
            vm.LoadData(); // если есть метод загрузки

            var mainWindow = new MainWindow
            {
                DataContext = vm
            };
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (Current.MainWindow?.DataContext is MainWindowViewModel vm)
            {
                MessageBox.Show($"Сохраняем {vm.Devices.Count} устройств");
                vm.SaveData();
            }
        }
    }

}
