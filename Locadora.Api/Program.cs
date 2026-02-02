using Microsoft.EntityFrameworkCore;
using Locadora.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicione esta linha para debug (remova depois)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(string.IsNullOrEmpty(connectionString))
    throw new ArgumentNullException(nameof(connectionString) + " String de conexão não encontrada --- " );
Console.WriteLine($"Connection String: [{connectionString}]");

// Add services to the container.

builder.Services.AddControllers();

// builder.Services.AddDbContext<LocadoraContext>(options => 
//     options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddDbContext<LocadoraContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 44)) // Ajuste para sua versão
    ));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
