**Панель мониторинга оборудования (WPF)**
Двухпанельное WPF-приложение для мониторинга и управления оборудованием.
Левая панель - сгруппированный список устройств
Правая панель - параметры устройства

#Архитектура
Приложение построено по паттерну MVVM:
Models
  Device
ViewModels
  MainWindowViewModel
  AddDeviceViewModel
  BaseViewModel
Views
  MainWindow.xaml
  AddDeviceWindow.xaml
Helpers
  RelayCommand
  EnumDisplayNameConverter
Data
  devices.json – файл для хранения данных устройств

#Установка и запуск
git clone WPF_EquipmentMonitor
Открывать в Visual Studio
