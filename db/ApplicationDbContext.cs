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
}