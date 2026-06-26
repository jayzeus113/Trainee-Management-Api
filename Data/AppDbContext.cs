using Microsoft.EntityFrameworkCore;
using TraineeManagement.Models;

namespace TraineeManagement.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trainee> Trainees { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Mentor> Mentors {get; set;} = null!;
    public DbSet<LearningTask> LearningTasks {get; set;} = null!;
    public DbSet<TaskAssignment> TaskAssignments {get; set;} = null!;
    public DbSet<Submission> Submissions {get; set;} = null!;
    public DbSet<Review> Reviews {get; set;} = null!;
    public DbSet<SubmissionFile> SubmissionFiles {get; set;} = null!;
    public DbSet<ProcessingJob> ProcessingJobs {get; set;} = null!;
}