using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Contexto;
using SistemaAlquilerDeComputadoras.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SistemaAlquilerDeComputadorasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaAlquilerDeComputadorasContext") ?? throw new InvalidOperationException("Connection string 'SistemaAlquilerDeComputadorasContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add connection string
builder.Services.AddDbContext<MyContext>(options => {
	options.UseSqlite(builder.Configuration.GetConnectionString("CadenaConexion"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalogo}/{action=Index}/{id?}");

app.Run();
