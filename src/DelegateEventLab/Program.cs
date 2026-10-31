
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var projector = new ProjectorRemote("PRJ-01", "Máy chiếu phòng A");
        var aircon = new AirConditionerRemote("AC-01", "Điều hòa phòng A", "A.101");
        var microphone = new WirelessMicrophone("MIC-01", "Micro không dây 01");
        TeachingDevice[] devices = [projector, aircon, microphone];
        var notifications = new ConsoleNotificationService();
        var audit = new DeviceAuditLogger();
        var maintenance = new MaintenanceService();
        using var coordinator = new DeviceMonitoringCoordinator(devices, notifications, audit, maintenance);
        coordinator.Subscribe();
        coordinator.Subscribe(); // Kiểm tra gọi lặp không tạo đăng ký trùng.

        Console.WriteLine("=== DEMO EVENTS ===");
        projector.ChangeStatus(DeviceStatus.Borrowed);
        projector.ChangeStatus(DeviceStatus.Borrowed); // Cùng trạng thái nên không phát event.
        projector.UpdateBatteryLevel(18);
        projector.UpdateBatteryLevel(0);
        aircon.UpdateResponseState(false);
        aircon.UpdateResponseState(false); // Không phát cảnh báo lặp.
        microphone.UpdateBatteryLevel(25);
        microphone.UpdateSignalQuality(35);

        int filteredAlerts = 0;
        var criticalOnly = new FilteredAlertSubscriber(
            alert => alert.Severity == AlertSeverity.Critical,
            alert => { filteredAlerts++; Console.WriteLine($"[FILTER CRITICAL] {alert.DeviceId}: {alert.Message}"); });
        microphone.AlertRaised += criticalOnly.HandleAlert;
        microphone.UpdateSignalQuality(0);
        microphone.AlertRaised -= criticalOnly.HandleAlert;

        Console.WriteLine($"Audit entries: {audit.Entries.Count}");
        Console.WriteLine($"Maintenance requests: {maintenance.Requests.Count}");
        Console.WriteLine("=== KIỂM THỬ ===");
        RunChecks(projector, aircon, microphone, audit, maintenance, filteredAlerts);

        coordinator.Unsubscribe();
        projector.ChangeStatus(DeviceStatus.Available);
        Console.WriteLine($"Audit sau Unsubscribe (không tăng): {audit.Entries.Count}");
    }

    private static void RunChecks(ProjectorRemote projector, AirConditionerRemote aircon, WirelessMicrophone microphone, DeviceAuditLogger audit, MaintenanceService maintenance, int filteredAlerts)
    {
        Check("Cập nhật trạng thái ghi đúng một log", audit.Entries.Count == 1);
        Check("Pin máy chiếu cảnh báo ngưỡng dưới 20 và mức 0", projector.BatteryLevel == 0 && maintenance.Requests.Any(x => x.Contains("PRJ-01")));
        Check("Mất phản hồi điều hòa chỉ tạo một cảnh báo bảo trì", maintenance.Requests.Count(x => x.Contains("AC-01")) == 1);
        Check("Micro mức cảnh báo thấp không tạo yêu cầu bảo trì", !maintenance.Requests.Any(x => x.Contains("MIC-01") && !x.Contains("Tín hiệu micro đã mất")));
        Check("Delegate chỉ nhận cảnh báo Critical", filteredAlerts == 1);
        CheckInvalid("Pin ngoài khoảng", () => microphone.UpdateBatteryLevel(101), "MIC-01", 101);
        CheckInvalid("Tín hiệu ngoài khoảng", () => microphone.UpdateSignalQuality(-1), "MIC-01", -1);
        Console.WriteLine("Ghi chú: có thể bổ sung kiểm thử biên cho mọi chỉ số theo cùng cách.");
    }

    private static void Check(string name, bool passed) => Console.WriteLine($"{(passed ? "PASS" : "FAIL")}: {name}");
    private static void CheckInvalid(string name, Action action, string expectedId, int expectedValue)
    {
        try { action(); Console.WriteLine($"FAIL: {name}"); }
        catch (InvalidDeviceReadingException e)
        {
            bool passed = e.DeviceId == expectedId && e.InvalidValue == expectedValue;
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")}: {name} (DeviceId={e.DeviceId}, Value={e.InvalidValue})");
        }
    }
}
