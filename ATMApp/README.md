Project ATM App 
1. .NET Core, Entity Framework Core(EF Core), MySQL,ORM và tích hợp Swagger. 
2. Cài đặt môi trường
 dotnet add package Microsoft.EntityFrameworkCore 
 dotnet add package Microsoft.EntityFramework.Tools
 dotnet add package Pomelo.EntityFrameworkCore.MySql

 Tich hop Swagger viết tài liệu API cho consumer 
 dotnet add package Swashbuckle.AspNetCore 
 3. Thực hiện Migration và tạo CSDL 
 dotnet ef migrations add InitialCreate (nguoc lai: dotnet restore)
 dotnet ef database update 
 dotnet run 
 