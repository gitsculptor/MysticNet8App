using Microsoft.AspNetCore.Identity;
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
        
        modelBuilder.Ignore<Company>();
        modelBuilder.Ignore<Employee>();
        
        // Configure custom table names with "Mystic" prefix
        modelBuilder.Entity<User>().ToTable("MysticUsers");
        modelBuilder.Entity<IdentityRole>().ToTable("MysticRoles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("MysticUserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("MysticUserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("MysticUserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("MysticUserTokens");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("MysticRoleClaims");

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
    
    public DbSet<Company>? Companies { get; set; }
    
    public DbSet<Employee>? Employees { get; set; }
   
    
    
    
}