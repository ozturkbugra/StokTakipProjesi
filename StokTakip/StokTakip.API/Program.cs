using Microsoft.EntityFrameworkCore;
using StokTakip.Core.Interfaces;
using StokTakip.Repository;
using StokTakip.Repository.Repositories;
using StokTakip.Service.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    // 1. appsettings.json'dan adresi al
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        // 2. Migration'lar Repository katmanýnda tutulacak de
        options.MigrationsAssembly("StokTakip.Repository");
    });
});

// 1. Generic Repository Tanýmý:
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 2. Generic Service Tanýmý:
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

// Add services to the container.

builder.Services.AddControllers();
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
