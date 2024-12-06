namespace HWBackend.DataAccess;

using Microsoft.EntityFrameworkCore;
using HWBackend.Models;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : base(options) => Database.EnsureCreated();

    public DbSet<Node> Nodes { get; set; } 
}