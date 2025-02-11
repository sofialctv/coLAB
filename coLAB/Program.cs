using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using colab.AutoMapper;
using colab.Business.Repository.Interfaces;
using colab.Business.Repository.Implementations;
using colab.Data;
using colab.Business.Services.Implementations;
using colab.Business.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


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
// Repositories
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IFinanciadorRepository, FinanciadorRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<IBolsaRepository, BolsaRepository>();
// Services
builder.Services.AddScoped<IBolsaService, BolsaService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IFinanciadorService, FinanciadorService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();


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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "coLAB API v1");
    });
}

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