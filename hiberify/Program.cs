using Microsoft.EntityFrameworkCore;
using webmusic_solved.Models;
using webmusic_solved.Services.CounterSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GrupoAContext>(options =>
{
    options.UseSqlServer("server=musicagrupos.databaase.windows.net;database=GrupoA;Integrated Security=True",
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(60),
                errorNumbersToAdd: null);
        });
});
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ICancionesService, CancionesService>();
builder.Services.AddScoped<ICountersqlService, CountersqlService>();
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