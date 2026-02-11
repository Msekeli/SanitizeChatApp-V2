
using Microsoft.EntityFrameworkCore;
using SanitizeChatApp.Infrastructure.Data;
using SanitizeChatApp.Infrastructure.Repositories;
using SanitizeChatApp.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Ensure App_Data exists
var appDataPath = Path.Combine(AppContext.BaseDirectory, "App_Data");
Directory.CreateDirectory(appDataPath);
var dbPath = Path.Combine(appDataPath, "sanitize.db");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);
builder.Services.AddScoped<ISensitiveWordRepository, SensitiveWordRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await SanitizeChatApp.Infrastructure.Data.WordSeeder.SeedAsync(db);
}

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
