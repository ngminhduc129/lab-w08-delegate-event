
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Hiển thị cảnh báo cho người vận hành.</summary>
public sealed class ConsoleNotificationService
{
    public void HandleAlert(object? sender, DeviceAlertEventArgs e) =>
        Console.WriteLine($"[{e.Severity}] {e.DeviceId}: {e.Message}");
}

/// <summary>Lưu lịch sử thay đổi trạng thái.</summary>
public sealed class DeviceAuditLogger
{
    private readonly List<string> entries = [];
    public IReadOnlyList<string> Entries => entries.AsReadOnly();
    public void HandleStatusChanged(object? sender, DeviceStatusChangedEventArgs e) =>
        entries.Add($"{e.ChangedAt:O}: {e.DeviceId}, {e.OldStatus} -> {e.NewStatus}");
}

/// <summary>Tạo yêu cầu bảo trì khi cảnh báo ở mức nghiêm trọng.</summary>
public sealed class MaintenanceService
{
    private readonly List<string> requests = [];
    public IReadOnlyList<string> Requests => requests.AsReadOnly();
    public void HandleCriticalAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (e.Severity == AlertSeverity.Critical) requests.Add($"Maintenance request for {e.DeviceId}: {e.Message}");
    }
}

public delegate bool AlertFilter(DeviceAlertEventArgs alert);

/// <summary>Callback chỉ thực hiện hành động khi dữ liệu thỏa bộ lọc delegate.</summary>
public sealed class FilteredAlertSubscriber(AlertFilter filter, Action<DeviceAlertEventArgs> action)
{
    public void HandleAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (filter(e)) action(e);
    }
}
