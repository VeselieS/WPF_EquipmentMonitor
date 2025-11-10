using System.Windows;
using WPF_EquipmentMonitor.ViewModels;

namespace WPF_EquipmentMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;

            var dialog = new Views.AddDeviceWindow();
            var addVm = new AddDeviceViewModel();
            dialog.DataContext = addVm;

            if (dialog.ShowDialog() == true)
            {
                var created = addVm.CreateDevice();
                vm.AddDevice(created);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DataContext is MainWindowViewModel vm && vm.HasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохранённые изменения. Сохранить перед выходом?",
                    "Подтверждение",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        vm.SaveEditingCommand.Execute(null);
                        break;
                    case MessageBoxResult.No:
                        // ничего не сохраняем
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true; // отменяем закрытие
                        break;
                }
            }
        }


    }
}