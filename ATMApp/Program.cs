using ATMApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Cấu hình DbContext với csdl 
builder.Services.AddDbContext<ATMContext>(options =>
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection")
    , new MySqlServerVersion(new Version(8, 0, 30))));
//Add controller 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//Cấu hình Swagger 
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo{
            Version = "v1",
            Title = "ATM Management API",
            Description = "API cho hệ thống ATM"
        });
    });
var app = builder.Build();

//Swagger in Development mode => for coder
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();