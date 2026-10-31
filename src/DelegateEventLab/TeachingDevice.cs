
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Lớp cơ sở công bố sự kiện mà không phụ thuộc vào subscriber.</summary>
public abstract class TeachingDevice
{
    public string Id { get; }
    public string Name { get; }
    public DeviceStatus Status { get; private set; } = DeviceStatus.Available;

    public event EventHandler<DeviceStatusChangedEventArgs>? StatusChanged;
    public event EventHandler<DeviceAlertEventArgs>? AlertRaised;

    protected TeachingDevice(string id, string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Id = id;
        Name = name;
    }

    /// <summary>Cập nhật trạng thái trước khi thông báo; trạng thái cũ thì không phát sự kiện.</summary>
    public void ChangeStatus(DeviceStatus newStatus)
    {
        if (!Enum.IsDefined(newStatus)) throw new ArgumentOutOfRangeException(nameof(newStatus));
        if (newStatus == Status) return;
        DeviceStatus oldStatus = Status;
        Status = newStatus;
        OnStatusChanged(new DeviceStatusChangedEventArgs(Id, oldStatus, newStatus, DateTime.Now));
    }

    protected void RaiseAlert(string message, AlertSeverity severity) =>
        OnAlertRaised(new DeviceAlertEventArgs(Id, message, severity, DateTime.Now));

    protected virtual void OnStatusChanged(DeviceStatusChangedEventArgs e) => StatusChanged?.Invoke(this, e);
    protected virtual void OnAlertRaised(DeviceAlertEventArgs e) => AlertRaised?.Invoke(this, e);
}
