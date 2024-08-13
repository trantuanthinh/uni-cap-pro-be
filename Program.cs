using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using uni_cap_pro_be;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

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
		connectionString = $"server={server};database={database};user={user};password={password};port={port}";
	}
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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

builder.Services.AddControllers(options =>
	options.Conventions.Add(new RoutePrefixConvention("/api"))
);

// Add Controllers with JSON options
builder.Services.AddControllers().AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
		options.JsonSerializerOptions.MaxDepth = 128;
	});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(o =>
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

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<JWTService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<SharedService>();
builder.Services.AddSingleton<API_ResponseConvention>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService<User>, UserService<User>>();
builder.Services.AddScoped<IProductService<Product>, ProductService<Product>>();
builder.Services.AddScoped<IProduct_CategoryService<Product_Category>, Product_CategoryService<Product_Category>>();
builder.Services.AddScoped<IProduct_ImageService<Product_Image>, Product_ImageService<Product_Image>>();
builder.Services.AddScoped<IBaseService<Order>, OrderService<Order>>();

builder.Services.AddScoped<DatabaseSeeder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower().Equals("seeddata"))
{
	SeedData(app);
}

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.Run();

static void SeedData(IHost app)
{
	using var scope = app.Services.CreateScope();
	var seed_service = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
	//var view_service = scope.ServiceProvider.GetRequiredService<DataContext>();

	seed_service.SeedDataContext();
	//view_service.CreateProductView();

	Console.WriteLine("Database seeded");
}
