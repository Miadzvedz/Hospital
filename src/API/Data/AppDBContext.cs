using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Data;

public class AppDBContext : DbContext
{
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Name> Names { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
