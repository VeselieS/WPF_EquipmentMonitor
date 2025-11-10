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

    public class Device
    {
        public string Category { get; set; } = "Без категории";
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public DeviceStatus Status { get; set; } = DeviceStatus.Working;
    }
}
