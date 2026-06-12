namespace TraineeManagementApi.db;

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Trainee> Trainees { get; set; }
    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Trainee>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<Trainee>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
    }
}