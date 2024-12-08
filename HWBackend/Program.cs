using HWBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLite");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();
app.Run();
