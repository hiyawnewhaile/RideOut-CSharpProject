#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace RideOut.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; } 
    public DbSet<Ride> Rides { get; set; } 
    public DbSet<Join> Joins { get; set; } 
    public DbSet<Bike> Bikes { get; set; } 
}