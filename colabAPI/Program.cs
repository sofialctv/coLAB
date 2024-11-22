using colabAPI.Business.Repository.Implementations;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Business.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// String de conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração do PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Solução para o erro 'System.Text.Json.JsonException: A possible object cycle was detected.' Lidando com Circular References enquanto relacionamentos são preservados.
builder.Services.AddControllers().AddJsonOptions(options =>
   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddScoped<IFinanciadorRepository, FinanciadorRepository>();
builder.Services.AddScoped<IBolsaRepository, BolsaRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<IOrientadorRepository, OrientadorRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "coLAB API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();