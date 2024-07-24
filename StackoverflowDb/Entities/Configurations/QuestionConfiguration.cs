using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoverflowDb.Entities.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(q => q.Title).HasMaxLength(50);
        builder.Property(q => q.PublicationDate).HasDefaultValueSql("getutcdate()");

        builder.HasMany(q => q.Comments)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionId);

        builder.HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);

        builder.HasMany(q => q.Tags)
            .WithMany(t => t.Questions)
            .UsingEntity<QuestionTag>(

            qt => qt.HasOne(q => q.Tag)
                .WithMany()
                .HasForeignKey(q => q.TagId),

            qt => qt.HasOne(t => t.Question)
                .WithMany()
                .HasForeignKey(t => t.QuestionId),

            qt =>
            {
                qt.HasKey(x => new { x.TagId, x.QuestionId });
            });
    }
}
