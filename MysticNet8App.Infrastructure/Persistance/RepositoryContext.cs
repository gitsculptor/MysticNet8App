using Microsoft.EntityFrameworkCore;
using MysticAppNet8App.Domain.Models;

namespace MysticNet8App.Infrastructure.Persistance;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }
    
    public DbSet<Company>? Companies { get; set; }
    
    public DbSet<Employee>? Employees { get; set; }
   
    
    
    
}