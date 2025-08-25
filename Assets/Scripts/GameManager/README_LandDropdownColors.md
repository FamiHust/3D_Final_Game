# Hệ thống Màu sắc Dropdown cho Lane Selection

## Tổng quan
Hệ thống này cho phép thay đổi màu sắc của các dropdown chọn Lane theo Element Type tương ứng:
- **Earth**: Xanh nhạt (0.7, 0.9, 0.7)
- **Water**: Xanh dương (0.4, 0.6, 1.0)  
- **Forest**: Xanh ngọc (0.4, 0.8, 0.6)
- **Swamp**: Xanh lá đậm (0.2, 0.6, 0.3)

## Cách hoạt động

### 1. Trạng thái ban đầu
- **Tất cả dropdown**: Màu trắng với text trắng
- **Các option trong dropdown**: Mỗi option có màu theo element type tương ứng

### 2. Khi mở dropdown
- **Options**: Giữ nguyên màu theo element type (Earth=xanh nhạt, Water=xanh dương, Forest=xanh ngọc, Swamp=xanh lá đậm)
- **Text của options**: Tất cả đều màu trắng

### 3. Khi chọn option
- **Dropdown chính**: Thay đổi màu theo element type đã chọn
- **Text và arrow của dropdown**: Luôn có màu trắng
- **Options**: Không bị thay đổi màu, vẫn giữ nguyên như ban đầu

### 4. Màu sắc tự động
- **Dropdown ban đầu**: Trắng với text trắng
- **Options trong dropdown**: Màu element type với text trắng (không thay đổi)
- **Dropdown sau khi chọn**: Màu element type đã chọn
- **Text và arrow**: Luôn có màu trắng để dễ đọc

## Các Script chính

### 1. SimpleLandDropdownColorizer.cs
Script chính để thay đổi màu sắc dropdown theo element type.

**Tính năng:**
- Dropdown ban đầu có màu trắng
- Các option trong dropdown có màu theo element type
- Dropdown sau khi chọn có màu theo element type đã chọn
- Tự động tính toán màu text tương phản

**Cách sử dụng:**
1. Thêm script vào GameObject trong scene
2. Gán các dropdown vào mảng `landDropdowns` (tùy chọn)
3. Điều chỉnh màu sắc trong Inspector nếu cần

### 2. LandSetupUI.cs (Đã cập nhật)
Script gốc đã được cập nhật để tích hợp với hệ thống màu sắc.

**Thay đổi:**
- Tự động tìm `SimpleLandDropdownColorizer`
- Cập nhật màu sắc khi dropdown thay đổi
- Refresh màu sắc khi khởi tạo
- **Sửa lỗi**: Sử dụng `UpdateDropdownColorByValue` để tránh lỗi index không khớp

### 3. LandDropdownColorDemo.cs
Script demo để test và debug hệ thống màu sắc.

**Tính năng:**
- Test refresh tất cả màu sắc về trạng thái ban đầu
- Test cycle qua các element colors
- Test reset tất cả dropdown về màu trắng
- Test `UpdateDropdownColorByValue` method
- Context menu để test từ Inspector

## Cách thiết lập trong Unity

### Bước 1: Thêm Scripts
1. Thêm `SimpleLandDropdownColorizer` vào GameObject trong scene
2. Thêm `LandDropdownColorDemo` nếu muốn test

### Bước 2: Cấu hình màu sắc
Trong Inspector của `SimpleLandDropdownColorizer`:

**Element Colors:**
- **Earth Color**: Xanh nhạt (0.7, 0.9, 0.7)
- **Water Color**: Xanh dương (0.4, 0.6, 1.0)
- **Forest Color**: Xanh ngọc (0.4, 0.8, 0.6)  
- **Swamp Color**: Xanh lá đậm (0.2, 0.6, 0.3)

**Default Colors:**
- **Default Dropdown Color**: Trắng (1.0, 1.0, 1.0)
- **Default Text Color**: Trắng (1.0, 1.0, 1.0)

### Bước 3: Gán Dropdowns (Tùy chọn)
- Kéo thả các dropdown vào mảng `landDropdowns`
- Nếu để trống, script sẽ tự động tìm tất cả dropdown trong scene

## Cách hoạt động chi tiết

### Khởi tạo
1. Script tự động tìm tất cả dropdown
2. Áp dụng màu trắng cho tất cả dropdown
3. Thiết lập màu sắc cho các option trong dropdown theo element type

### Khi mở dropdown
1. Mỗi option hiển thị với màu sắc tương ứng
2. Text color tự động điều chỉnh để dễ đọc
3. Background của option có màu theo element type

### Khi thay đổi giá trị
1. `LandSetupUI` gọi `UpdateDropdownColorByValue()`
2. Màu sắc của dropdown được cập nhật theo element type mới
3. Text và arrow tự động điều chỉnh màu tương phản

### Màu sắc tự động
- **Dropdown ban đầu**: Trắng với text trắng
- **Options trong dropdown**: Màu element type với text trắng
- **Dropdown sau khi chọn**: Màu element type đã chọn
- **Text và arrow**: Luôn có màu trắng để dễ đọc

## Troubleshooting

### Dropdown không đổi màu
1. Kiểm tra script có được gán đúng không
2. Kiểm tra dropdown có component `Image` không
3. Kiểm tra Console có lỗi gì không

### Màu sắc không đúng
1. Kiểm tra giá trị màu trong Inspector
2. Kiểm tra thứ tự dropdown có khớp với element type không
3. Sử dụng `RefreshAllColors()` để reset về trạng thái ban đầu

### Lỗi index không khớp (ĐÃ SỬA)
**Triệu chứng**: Khi chọn dropdown đầu tiên, dropdown thứ hai lại đổi màu
**Nguyên nhân**: Index của dropdown không khớp với index trong mảng
**Cách sửa**: Sử dụng `UpdateDropdownColorByValue()` thay vì `UpdateDropdownColor()`
**Trạng thái**: Đã được sửa trong phiên bản hiện tại

### Performance
- Script chỉ chạy khi cần thiết
- Không có update loop liên tục
- Tự động tối ưu hóa

## API Reference

### SimpleLandDropdownColorizer
```csharp
// Cập nhật màu dropdown cụ thể theo element type (theo index)
public void UpdateDropdownColor(int dropdownIndex, ElementType elementType)

// Cập nhật màu dropdown theo dropdown value (khuyến nghị sử dụng)
public void UpdateDropdownColorByValue(TMP_Dropdown dropdown, int dropdownValue)

// Reset dropdown về màu trắng
public void ResetDropdownToDefault(int dropdownIndex)

// Refresh tất cả màu sắc về trạng thái ban đầu
public void RefreshAllColors()

// Reset tất cả dropdown về màu trắng
public void ResetAllDropdownsToDefault()
```

### LandDropdownColorDemo
```csharp
// Test methods (Context Menu)
public void TestRefreshColors()
public void TestResetAllToDefault()
public void TestUpdateDropdownColorByValue()  // Test method mới
public void TestEarthColors()
public void TestWaterColors()
public void TestForestColors()
public void TestSwampColors()
public void TestIndividualDropdownColors()
```

## Lưu ý quan trọng

### Về việc sửa lỗi index
- **Trước đây**: Sử dụng `UpdateDropdownColor(index, elementType)` có thể gây lỗi index không khớp
- **Hiện tại**: Sử dụng `UpdateDropdownColorByValue(dropdown, value)` để đảm bảo đúng dropdown được cập nhật
- **Khuyến nghị**: Luôn sử dụng method mới để tránh lỗi

### Về màu sắc
- **Ban đầu**: Tất cả dropdown có màu trắng với text trắng
- **Options**: Mỗi option có màu theo element type tương ứng
- **Sau khi chọn**: Dropdown có màu theo element type đã chọn
- Script tự động tìm dropdown nếu không được gán
- Màu sắc được áp dụng cho tất cả thành phần của dropdown
- Text color tự động tính toán để đảm bảo khả năng đọc
- Hỗ trợ cả TMP_Dropdown và Dropdown thông thường
