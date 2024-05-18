using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
public AppDbContext()
{
}
public AppDbContext(DbContextOptions<AppDbContext> options) : 
base(options)
{
}
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var builder = WebApplication.CreateBuilder();
    var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection");
    optionsBuilder.UseSqlServer(connectionString);
}
public DbSet<Room> Rooms { get; set; }
public DbSet<Reservation> Reservations { get; set; }
}