using Microsoft.EntityFrameworkCore;

namespace StackoverflowDb.Entities;

public class StackoverflowDbContext : DbContext
{
    public StackoverflowDbContext(DbContextOptions<StackoverflowDbContext> options)
        :base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<QuestionTag> QuestionTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
