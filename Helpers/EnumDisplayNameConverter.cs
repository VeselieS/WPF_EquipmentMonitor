using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace WPF_EquipmentMonitor.Helpers
{
    public class EnumDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var field = value.GetType().GetField(value.ToString());
            var display = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                               .Cast<DisplayAttribute>()
                               .FirstOrDefault();
            return display?.Name ?? value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Попытка найти enum по DisplayAttribute.Name или по имени
            if (value == null || !targetType.IsEnum) return null;
            foreach (var field in targetType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var display = field.GetCustomAttribute<DisplayAttribute>();
                if (display?.Name == value.ToString())
                    return Enum.Parse(targetType, field.Name);
                if (field.Name == value.ToString())
                    return Enum.Parse(targetType, field.Name);
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
