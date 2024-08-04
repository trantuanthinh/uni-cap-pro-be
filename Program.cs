using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Repositories;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
		{
			builder.AllowAnyOrigin()
				   .AllowAnyMethod()
				   .AllowAnyHeader();
		});
});

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
{
	x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<SharedService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer();





var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
