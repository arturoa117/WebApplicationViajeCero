using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Seeders.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ApplicationBuilder>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MySql conn


string _GetConnetionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseMySql(_GetConnetionString, ServerVersion.AutoDetect(_GetConnetionString)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    ProvinceSeeder.Seed(context);
}

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
