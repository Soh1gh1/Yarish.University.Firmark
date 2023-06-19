using Yarish.University.Filmark.Core;
using Yarish.University.Filmark.Database;
using Microsoft.AspNetCore.MiddlewareAnalysis;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.RegisterCoreConfiguration(builder.Configuration);
builder.Services.RegisterCoreDependencies();

builder.Services.RegisterDatabaseDependencies(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Зареєструвати служби сеансу
builder.Services.AddDistributedMemoryCache(); // Додайте реалізацію розподіленого кешу, якщо потрібно
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Встановіть тривалість тайм-ауту сеансу за потреби
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Налаштувати конвеєр обробки HTTP-запитів.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
