using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TanPhucShopApi.Converter;
using TanPhucShopApi.Data;
using TanPhucShopApi.Mapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Services.CategoryService;
using TanPhucShopApi.Services.ProductService;
using TanPhucShopApi.Services.RoleService;
using TanPhucShopApi.Services.UserService;
using FluentValidation.AspNetCore;
using TanPhucShopApi.Validatiors.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(opts => opts.RegisterValidatorsFromAssembly(typeof(RegisterUserDtoValidator).Assembly)).AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new Dateconverter());
}); ; ;
builder.Services.AddDbContext<AppDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddIdentity<User,Role>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(RoleMapper));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<RoleManager<Role>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IInvoiceService, InvoiceService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Thiết lập về Password
//    options.Password.RequireDigit = true; // Không bắt phải có số
//    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
//    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
//    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
//    options.Password.RequiredLength = 6; // Số ký tự tối thiểu của password
//    options.Password.RequiredUniqueChars = 0; // Số ký tự riêng biệt

//    // Cấu hình Lockout - khóa user
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
//    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
//    options.Lockout.AllowedForNewUsers = true;

//    // Cấu hình về User.
//    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
//        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = true;  // Email là duy nhất
//});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
