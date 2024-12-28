using Api.Presentation.Middleware;
using Application.UseCase;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Infrastructure.Context;
using Infrastructure.RepositorySqlServer;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreate, Create>();
builder.Services.AddScoped<IGetAll, GetAll>();
builder.Services.AddScoped<IGetById, GetById>();
builder.Services.AddScoped<IUpdate, Update>();
builder.Services.AddScoped<IDelete, Delete>();
builder.Services.AddScoped<DbContextSqlServer>();
builder.Services.AddScoped<IRepository, Repository>();

string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
    builder.Configuration.GetConnectionString("CadenaSQL");

builder.Services.AddDbContext<DbContextSqlServer>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
