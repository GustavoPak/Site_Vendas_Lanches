using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Vendas_Site.Context;
using Vendas_Site.Models;
using Vendas_Site.Repositories;
using Vendas_Site.Repositories.Interfaces;
using Vendas_Site.Services;
using Microsoft.Extensions.Hosting;
using System.Drawing.Text;
using ReflectionIT.Mvc.Paging;
using Vendas_Site.Servicos;
using Vendas_Site.Areas.Admin.Servicos;

var StringConnection = "Server=DESKTOP-F2T5O8F\\SQLEXPRESS;Database=LanchesFinal;Trusted_Connection=True;TrustServerCertificate=true;";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(StringConnection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();


builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddTransient<ILancheRepository,LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinhoCompra(sp));
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();

builder.Services.AddAuthorization(options =>
  options.AddPolicy("Admin", p =>
  p.RequireRole("Admin")
  ));

builder.Services.AddMemoryCache();

builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options => {
    options.ViewName = "Bootstrap5";
    options.PageParameterName = "pageindex";
});

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

app.UseSession();

// Obtenha uma instância do serviço a partir do provedor de serviços raiz
using (var scope = app.Services.CreateScope())
{
    var meuServico = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();

    // Chame o método desejado na sua interface personalizada
    meuServico.SeedRoles();
    meuServico.SeedUsers();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
   name: "categoriaFiltro",
   pattern: "Lanche/{action}/{categoria?}",
   defaults: new { Controller = "Lanche", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
