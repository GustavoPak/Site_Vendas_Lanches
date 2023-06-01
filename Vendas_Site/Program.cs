using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Vendas_Site.Context;
using Vendas_Site.Models;
using Vendas_Site.Repositories;
using Vendas_Site.Repositories.Interfaces;

var StringConnection = "Server=DESKTOP-F2T5O8F\\SQLEXPRESS;Database=LanchesFinal;Trusted_Connection=True;TrustServerCertificate=true;";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(StringConnection));

builder.Services.AddTransient<ILancheRepository,LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinhoCompra(sp));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "CategoriaFiltro",
    pattern: "lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", Action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
