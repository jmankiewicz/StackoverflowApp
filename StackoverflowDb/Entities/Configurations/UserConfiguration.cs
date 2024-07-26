using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoverflowDb.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).IsRequired();

        builder.Property(u => u.Nickname).HasMaxLength(16);

        builder.Property(u => u.Email).HasMaxLength(32);

        builder.HasMany(u => u.Questions)
            .WithOne(q => q.Author)
            .HasForeignKey(q => q.AuthorId);

        builder.HasMany(u => u.Answers)
            .WithOne(a => a.Author)
            .HasForeignKey(a => a.AuthorId);

        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId);
    }
}
