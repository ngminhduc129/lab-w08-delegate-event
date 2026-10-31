
// /****************/
// Mã sinh viên: [ĐIỀN MÃ SINH VIÊN]
// Họ tên: [ĐIỀN HỌ TÊN]
// /****************/

namespace DelegateEventLab;

/// <summary>Ngoại lệ cho chỉ số pin hoặc tín hiệu ngoài khoảng 0-100.</summary>
public sealed class InvalidDeviceReadingException : Exception
{
    public string DeviceId { get; }
    public int InvalidValue { get; }

    public InvalidDeviceReadingException(string deviceId, int invalidValue, string readingName)
        : base($"{readingName} của thiết bị {deviceId} phải trong khoảng 0-100; nhận được {invalidValue}.")
    {
        DeviceId = deviceId;
        InvalidValue = invalidValue;
    }
}
