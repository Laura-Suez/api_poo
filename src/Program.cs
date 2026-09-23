using api_poo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api_poo.Entities;
using api_poo.Models;
using api_poo.Data; 
using Microsoft.EntityFrameworkCore;
using api_poo.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Registrar el servicio en el contenedor de IoC
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepositoryEF>();
builder.Services.AddScoped<TransferService>();

builder.Services.AddControllersWithViews();

var connection = new SqliteConnection("Data Source=Bank.db");
connection.Open();

// Set journal mode to DELETE using PRAGMA statement
using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<AppDbContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));

var app = builder.Build();

#region Apply EF migrations
using (var serviceScopescope = app.Services.CreateScope())
{
    var dbContext = serviceScopescope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();