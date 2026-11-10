using APP.Domain;
using APP.Models;
using APP.Services;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC (Inversion of Control) container.
builder.Services.AddDbContext<DbContext, Db>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString(nameof(Db))
));
builder.Services.AddScoped<IService<GenreRequest, GenreResponse>, GenreService>();
builder.Services.AddScoped<IService<AuthorRequest, AuthorResponse>, AuthorService>();
builder.Services.AddScoped<IService<BookRequest, BookResponse>, BookService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
