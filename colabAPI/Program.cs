using colabAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using colabAPI.AutoMapper;
using colabAPI.Business.Repository.Implementations;
using colabAPI.Business.Repository.Interfaces;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// String de conexão com o banco de dados
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração do PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Solução para o erro 'System.Text.Json.JsonException: A possible object cycle was detected.' Lidando com Circular References enquanto relacionamentos são preservados.
builder.Services.AddControllers().AddJsonOptions(options =>
   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// AutoMapper
builder.Services.AddAutoMapper(typeof(ConfigMapping));

// Injeção de dependências
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IHistoricoCargoRepository, HistoricoCargoRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar serviços ao contêiner
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Access-Control-Allow-Origin", "Access-Control-Allow-Headers", "Access-Control-Allow-Methods");
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Referrer-Policy", "no-referrer-when-downgrade");
    await next();
});

// Habilita CORS
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();
