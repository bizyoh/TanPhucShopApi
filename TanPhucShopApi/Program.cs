using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TanPhucShopApi.Data;
using TanPhucShopApi.Mapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Services.CategoryService;
using TanPhucShopApi.Services.ProductService;
using TanPhucShopApi.Services.RoleService;
using TanPhucShopApi.Services.UserService;
using FluentValidation.AspNetCore;
using TanPhucShopApi.Validatiors.User;
using TanPhucShopApi.Middleware;
using TanPhucShopApi.Services.InvoiceService;
using IntegrationTest;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddFluentValidation(opts => opts.RegisterValidatorsFromAssembly(typeof(RegisterUserDtoValidator).Assembly))
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<AppDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
builder.Services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(RoleMapper));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IInvoiceService, InvoiceService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

//var identityBuilder = builder.Services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<AppDBContext>();
//if (builder.Environment.IsEnvironment("Testing"))
//{
//    identityBuilder.AddUserManager<FakeUserManager>();
//}

if (!builder.Environment.IsEnvironment("Testing"))
{
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
}

builder.Services.AddSwaggerGen();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }