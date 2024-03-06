using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Infrastructure.Configurations;

namespace MysticNet8App.Infrastructure.Persistance;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
    
    public DbSet<Company>? Companies { get; set; }
    
    public DbSet<Employee>? Employees { get; set; }
   
    
    
    
}