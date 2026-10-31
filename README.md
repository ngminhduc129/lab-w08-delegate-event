# Lab W08 - Delegate & Event

Ứng dụng console C# minh họa quản lý thiết bị giảng dạy bằng event và delegate.

## Chạy chương trình

```powershell
dotnet run --project src/DelegateEventLab/DelegateEventLab.csproj
```

## Nội dung

- Thiết bị: ProjectorRemote, AirConditionerRemote, WirelessMicrophone.
- Event: StatusChanged, AlertRaised; dữ liệu event và ngoại lệ chỉ số không hợp lệ.
- Subscriber: thông báo console, audit logger, maintenance service.
- Delegate callback: lọc cảnh báo Critical.
- Coordinator: đăng ký/hủy đăng ký subscriber, không đăng ký trùng và hỗ trợ Dispose.
- PDF báo cáo: sơ đồ lớp, giải thích thiết kế, kết quả chạy và kiểm thử.

## Thông tin sinh viên

Mã sinh viên: `[ĐIỀN MÃ SINH VIÊN]`  
Họ tên: `[ĐIỀN HỌ TÊN]`

## Cập nhật thông tin GitHub trong báo cáo

Sau khi điền mã sinh viên và họ tên vào các tệp `.cs` và PDF, thay URL repository thực tế ở trang đầu PDF.
