using colabAPI.Data;
using Microsoft.EntityFrameworkCore;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Business.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// configura��o do PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//ta certo nao
builder.Services.AddScoped<IBolsistaRepository, BolsistaRepository>();

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