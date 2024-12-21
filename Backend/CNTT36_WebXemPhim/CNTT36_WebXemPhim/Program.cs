using CNTT36_WebXemPhim.Mappings;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ vào container
builder.Services.AddControllers();

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IEmailRepository, EmailRepository>();


#region Config

// Cấu hình Swagger với hỗ trợ JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SellCourse API", Version = "v1" });

    // Cấu hình JWT trong Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new string[] {}
        }
    });
});

// Cấu hình CORS cho phép tất cả nguồn gốc
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Cấu hình DbContext sử dụng SQL Server
builder.Services.AddDbContext<SellCourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
);

// Đăng ký các dịch vụ Repository
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICateRepository, CateRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IRoleCourseRepository, RoleCourseRepository>();
builder.Services.AddScoped<ITeachersRoleCourseRepository, TeachersRoleCoursesRepository>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IRoleCourseRepository, RoleCourseRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IDatabaseBackupRepository, DatabaseBackupRepository>();

builder.Services.AddDbContext<SellCourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

//builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

// Cấu hình AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Cấu hình JSON serializer để tránh lỗi tham chiếu vòng lặp
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // Đổi từ Preserve sang IgnoreCycles
});

// Cấu hình xác thực JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication(); // Đảm bảo xác thực được sử dụng
app.UseAuthorization(); // Đảm bảo phân quyền được sử dụng

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/admin") && !context.User.IsInRole("ADMIN"))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden; // Trả về Forbidden
        await context.Response.WriteAsync("You do not have permission to access this page."); // Thông báo lỗi
        return;
    }

    await next();
});


app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseStaticFiles();

app.UseAuthentication(); // Sử dụng xác thực JWT
app.UseAuthorization();  // Sử dụng quyền truy cập

app.MapControllers();

app.Run();
