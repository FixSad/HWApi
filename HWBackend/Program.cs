using HWBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLite");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy( policy => 
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.MapControllers();
app.Run();
