# Hướng dẫn Setup Dự án Web Khóa Học Võ Thuật
Các bước set up
## Clone dự án về máy

Vào thư mục Backend -> SellCourse -> Double click vào SellCourse.sln để mở dự án

Mở Tool:

1. Chọn NuGet -> Package Manager Console
2. Setup cơ sở dữ liệu (CSDL) bằng lệnh sau:
3. Update-Database -Context SellCourse
4. Lệnh này dùng để khởi tạo cơ sở dữ liệu từ code vào SQL Server.

## Nếu báo lỗi:
1. Vào file appsettings.json và SellCourseContext trong thư mục Models.
2. Đổi tên server, tài khoản, và mật khẩu (sa) đúng với máy bạn.
3. Sau đó chạy lại lệnh trên.
## Nếu cần tạo hoặc sửa bảng trong CSDL và cập nhật lại Models:
Chạy lệnh sau:
1. Scaffold-DbContext "Server=localhost;Database=OnlyFilm;User=Sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables est1
2. Lưu ý: Đổi server, user, và password phù hợp với máy của bạn.
## Thêm dữ liệu theo thứ tự:
1. Bảng Courser -> Lesson: Để thêm khóa học mới.
2. Tạo tài khoản trong bảng User_Role:
- Nhập username vừa tạo.
- Nhập role_id = 2.
3. Đăng nhập với tài khoản mới tại: /admin/login 
Để thực hiện các thao tác trên giao diện.
