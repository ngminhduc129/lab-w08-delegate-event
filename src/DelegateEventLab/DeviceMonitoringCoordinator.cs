
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Tập trung đăng ký và hủy đăng ký các subscriber của thiết bị.</summary>
public sealed class DeviceMonitoringCoordinator : IDisposable
{
    private readonly List<TeachingDevice> devices;
    private readonly ConsoleNotificationService notificationService;
    private readonly DeviceAuditLogger auditLogger;
    private readonly MaintenanceService maintenanceService;
    private bool isSubscribed;

    public DeviceMonitoringCoordinator(IEnumerable<TeachingDevice> devices, ConsoleNotificationService notificationService, DeviceAuditLogger auditLogger, MaintenanceService maintenanceService)
    {
        this.devices = devices.Distinct().ToList();
        this.notificationService = notificationService;
        this.auditLogger = auditLogger;
        this.maintenanceService = maintenanceService;
    }

    public void Subscribe()
    {
        if (isSubscribed) return;
        foreach (TeachingDevice device in devices)
        {
            device.AlertRaised += notificationService.HandleAlert;
            device.AlertRaised += maintenanceService.HandleCriticalAlert;
            device.StatusChanged += auditLogger.HandleStatusChanged;
        }
        isSubscribed = true;
    }

    public void Unsubscribe()
    {
        if (!isSubscribed) return;
        foreach (TeachingDevice device in devices)
        {
            device.AlertRaised -= notificationService.HandleAlert;
            device.AlertRaised -= maintenanceService.HandleCriticalAlert;
            device.StatusChanged -= auditLogger.HandleStatusChanged;
        }
        isSubscribed = false;
    }

    public void Dispose() => Unsubscribe();
}
