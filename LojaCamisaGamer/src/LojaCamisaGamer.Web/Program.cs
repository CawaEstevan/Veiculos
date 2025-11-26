// src/LojaCamisaGamer.Web/Program.cs
using Microsoft.EntityFrameworkCore;
using LojaCamisaGamer.Infrastructure.Data;
using LojaCamisaGamer.Domain.Interfaces;
using LojaCamisaGamer.Infrastructure.Repositories;
using LojaCamisaGamer.Application.Interfaces;
using LojaCamisaGamer.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ⚠️ IMPORTANTE: Usar UseSqlServer (não SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependências - Repositories
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICamisaRepository, CamisaRepository>();

// Injeção de Dependências - Services
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICamisaService, CamisaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();