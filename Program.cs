using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using uni_cap_pro_be;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Repositories;
using uni_cap_pro_be.Repositories.Setting_Data_Repositories;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Services.Setting_Data_Services;
using uni_cap_pro_be.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!builder.Environment.IsDevelopment())
    {
        var server = builder.Configuration["DB_HOST"] ?? "localhost";
        var database = builder.Configuration["DB_NAME"] ?? "agrimart";
        var user = builder.Configuration["DB_USER"] ?? "root";
        var port = builder.Configuration["DB_PORT"] ?? "3306";
        var password = builder.Configuration["DB_PASSWORD"] ?? "trantuanthinh";
        connectionString =
            $"server={server};database={database};user={user};password={password};port={port}";
    }
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Controllers with JSON options and route prefix
builder
    .Services.AddControllers(options =>
    {
        options.Conventions.Add(new RoutePrefixConvention("/api"));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.MaxDepth = 128;
    });

// Add authentication and JWT configuration
builder.Services.AddAuthorization();
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

// Add other services
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<JWTService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<BaseResponse<object>>();
builder.Services.AddSingleton<SharedService>();
builder.Services.AddSingleton<APIResponse>();
builder.Services.AddSingleton<MailService>();
builder.Services.AddSingleton<OtpService>();
builder.Services.AddSingleton<ReaderCsv>();

builder.Services.AddScoped<BaseAPIController>();

// Setting Data
builder.Services.AddScoped<UnitMeasureRepository>();
builder.Services.AddScoped<UnitMeasureService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ProvinceRepository>();
builder.Services.AddScoped<DistrictRepository>();
builder.Services.AddScoped<WardRepository>();

builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<Product_ImageRepository>();
builder.Services.AddScoped<Product_ImageService>();

builder.Services.AddScoped<Product_CategoryRepository>();
builder.Services.AddScoped<Product_CategoryService>();

builder.Services.AddScoped<DiscountRepository>();
builder.Services.AddScoped<DiscountService>();

builder.Services.AddScoped<Discount_DetailRepository>();
builder.Services.AddScoped<Discount_DetailService>();

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<Sub_OrderRepository>();
builder.Services.AddScoped<Sub_OrderService>();

builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<FeedbackService>();

builder.Services.AddScoped<DatabaseSeeder>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower().Equals("seed"))
{
    SeedData(app);
}

// Configure the HTTP request pipeline
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.Run();

static void SeedData(IHost app)
{
    using var scope = app.Services.CreateScope();
    var seed_service = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    seed_service.SeedDataContext();

    Console.WriteLine("Database seeding completed.");
}
