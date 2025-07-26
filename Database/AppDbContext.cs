using Microsoft.EntityFrameworkCore;
using NewMission.Models;


namespace NewMission.Database
{
    public sealed class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> confiq)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //You can change ur database connection string 
            options.UseSqlServer(Connections.SqlConStr);
            //options.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "development.db")}");
            
         }
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Mission> Missions => Set<Mission>();
    }
}
