using Yarish.University.Filmark.Core;
using Yarish.University.Filmark.Database;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.RegisterCoreConfiguration(builder.Configuration);
builder.Services.RegisterCoreDependencies();

// Register database dependencies
builder.Services.RegisterDatabaseDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
