using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Data; // ← Data klasörün varsa bu using gerekir

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı (appsettings.json'daki "DefaultConnection" kullanılır)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC controller + view desteği
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata sayfası ve HTTPS ayarları
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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
