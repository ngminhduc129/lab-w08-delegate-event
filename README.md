# Lab W08 - Delegate & Event

Ứng dụng console C# minh họa quản lý thiết bị giảng dạy bằng event và delegate.

GitHub: https://github.com/ngminhduc129/lab-w08-delegate-event

## Chạy chương trình

```powershell
dotnet run --project src/DelegateEventLab/DelegateEventLab.csproj
```

## Thành phần chính

- `TeachingDevice`: lớp cơ sở và hai event `StatusChanged`, `AlertRaised`.
- `ProjectorRemote`, `AirConditionerRemote`, `WirelessMicrophone`: publisher cụ thể.
- `ConsoleNotificationService`, `DeviceAuditLogger`, `MaintenanceService`: subscriber.
- `FilteredAlertSubscriber`: callback lọc cảnh báo bằng delegate.
- `DeviceMonitoringCoordinator`: quản lý đăng ký/hủy đăng ký subscriber.
- `InvalidDeviceReadingException`: báo chỉ số ngoài khoảng 0-100.

## Cập nhật và đẩy thay đổi lên GitHub

Sau khi điền mã sinh viên/họ tên hoặc sửa mã nguồn, mở PowerShell tại thư mục này và chạy:

```powershell
git add .
git commit -m "Update lab W08 submission"
git push
```

Trước khi nộp, điền mã sinh viên và họ tên ở đầu các file `.cs` và trang đầu PDF.
