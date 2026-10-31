
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Dữ liệu được gửi kèm khi trạng thái thiết bị thay đổi.</summary>
public sealed class DeviceStatusChangedEventArgs : EventArgs
{
    public string DeviceId { get; }
    public DeviceStatus OldStatus { get; }
    public DeviceStatus NewStatus { get; }
    public DateTime ChangedAt { get; }

    public DeviceStatusChangedEventArgs(string deviceId, DeviceStatus oldStatus, DeviceStatus newStatus, DateTime changedAt)
    {
        DeviceId = deviceId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = changedAt;
    }
}

/// <summary>Dữ liệu được gửi kèm khi thiết bị phát cảnh báo.</summary>
public sealed class DeviceAlertEventArgs : EventArgs
{
    public string DeviceId { get; }
    public string Message { get; }
    public AlertSeverity Severity { get; }
    public DateTime OccurredAt { get; }

    public DeviceAlertEventArgs(string deviceId, string message, AlertSeverity severity, DateTime occurredAt)
    {
        DeviceId = deviceId;
        Message = message;
        Severity = severity;
        OccurredAt = occurredAt;
    }
}
