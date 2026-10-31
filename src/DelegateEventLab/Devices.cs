
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Điều khiển máy chiếu và theo dõi pin.</summary>
public sealed class ProjectorRemote(string id, string name) : TeachingDevice(id, name)
{
    public int BatteryLevel { get; private set; } = 100;
    public void UpdateBatteryLevel(int newLevel)
    {
        DeviceReadingValidator.Validate(Id, newLevel, "Pin");
        int old = BatteryLevel; BatteryLevel = newLevel;
        if (newLevel == 0 && old != 0) RaiseAlert("Pin máy chiếu đã hết.", AlertSeverity.Critical);
        else if (newLevel < 20 && old >= 20) RaiseAlert($"Pin máy chiếu còn {newLevel}%.", AlertSeverity.Warning);
    }
}

/// <summary>Điều khiển điều hòa và phát cảnh báo khi mất phản hồi.</summary>
public sealed class AirConditionerRemote(string id, string name, string assignedRoom) : TeachingDevice(id, name)
{
    public string AssignedRoom { get; } = assignedRoom;
    public bool IsResponsive { get; private set; } = true;
    public void UpdateResponseState(bool isResponsive)
    {
        bool wasResponsive = IsResponsive; IsResponsive = isResponsive;
        if (wasResponsive && !isResponsive) RaiseAlert($"Điều hòa tại phòng {AssignedRoom} không phản hồi.", AlertSeverity.Critical);
    }
}

/// <summary>Micro không dây theo dõi pin và chất lượng tín hiệu.</summary>
public sealed class WirelessMicrophone(string id, string name) : TeachingDevice(id, name)
{
    public int BatteryLevel { get; private set; } = 100;
    public int SignalQuality { get; private set; } = 100;
    public void UpdateBatteryLevel(int newLevel)
    {
        DeviceReadingValidator.Validate(Id, newLevel, "Pin");
        int old = BatteryLevel; BatteryLevel = newLevel;
        if (newLevel == 0 && old != 0) RaiseAlert("Pin micro đã hết.", AlertSeverity.Critical);
        else if (newLevel < 30 && old >= 30) RaiseAlert($"Pin micro còn {newLevel}%.", AlertSeverity.Warning);
    }
    public void UpdateSignalQuality(int newQuality)
    {
        DeviceReadingValidator.Validate(Id, newQuality, "Chất lượng tín hiệu");
        int old = SignalQuality; SignalQuality = newQuality;
        if (newQuality == 0 && old != 0) RaiseAlert("Tín hiệu micro đã mất.", AlertSeverity.Critical);
        else if (newQuality < 40 && old >= 40) RaiseAlert($"Chất lượng tín hiệu micro còn {newQuality}%.", AlertSeverity.Warning);
    }
}

/// <summary>Dùng chung kiểm tra biên cho mọi chỉ số thiết bị.</summary>
internal static class DeviceReadingValidator
{
    public static void Validate(string id, int value, string name)
    {
        if (value is < 0 or > 100) throw new InvalidDeviceReadingException(id, value, name);
    }
}
