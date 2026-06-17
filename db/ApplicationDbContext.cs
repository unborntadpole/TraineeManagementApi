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
    public DbSet<LearningTask> LearningTasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.LearningTask)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.LearningTaskId)
            .IsRequired();
        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.Trainee)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.TraineeId)
            .IsRequired();
        modelBuilder.Entity<TaskAssignment>()
            .HasOne(e => e.Mentor)
            .WithMany(e => e.TaskAssignments)
            .HasForeignKey(e => e.MentorId)
            .IsRequired();

        modelBuilder.Entity<Submission>()
            .HasOne(e => e.TaskAssignment)
            .WithOne(e => e.Submission)
            .HasForeignKey<Submission>(e => e.TaskAssignmentId)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .HasOne(e => e.Submission)
            .WithOne(e => e.Review)
            .HasForeignKey<Review>(e => e.SubmissionId)
            .IsRequired();
        modelBuilder.Entity<Review>()
            .HasOne(e => e.Mentor)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.MentorId)
            .IsRequired();

        // modelBuilder.Entity<User>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();
        
        // modelBuilder.Entity<Trainee>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();
            
        // modelBuilder.Entity<Mentor>()
        //     .Property(u => u.Id)
        //     .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1,
                Email = "samriddh.singh@zeuslearning.com", 
                Username = "admin", 
                PasswordHash = "AQAAAAIAAYagAAAAEP+QfNdJZtmZSCQUsvRTWt8NlKADYbY44q8GjYNIUhVn8c2ANxKiw50h4muvwf7ydg==", 
                Role = "Admin", 
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        );
    }
}

