using Microsoft.EntityFrameworkCore;
using TarefasMvc.EfConfigs;
using TarefasMvc.Models;

namespace TarefasMvc.Context;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos => Set<Todo>();
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TodoContextConfiguration());
    }
}