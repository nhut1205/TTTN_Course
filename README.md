Web khóa học võ thuật
Các bước set up
1 : Clone dự án về
2 : Vào Backend -> SellCourse -> SellCourser.sln double click để mở 
3 : Mở tool -> nuget -> package manager  Set up csdl bằng lệnh sau :
Update-Database -Context SellCourse để khởi tạo db từ code vào sql server
Trường hợp báo lỗi thì vào appsetting.js và sellcoursecontext trong models đổi tên server, tài khoản mk sa đúng với máy
Sau đó chạy lại lệnh trên
Trường hợp tạo hoặc sửa bảng trong csdl cần uodate lại models
Chạy lệnh
Scaffold-DbContext "Server=localhost;Database=OnlyFilm;User=Sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables est1
Nhớ đổi tên và mk cho phù hợp máy

Insert db theo thứ tự
Bảng Courser -> Lesson để thêm khóa học mới hoặc tạo tài khoản vào user_role nhập username vừa tạo, nhập role_id 2 login vào trang /admin/login để thực hiện trên giao diện

Project Final Course
