using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoverflowDb.Entities.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(
                new Tag
                {
                    Id = 1,
                    Name = "Programming"
                },
                new Tag
                {
                    Id = 2,
                    Name = "Artificial Intelligence"
                },
                new Tag
                {
                    Id = 3,
                    Name = "C# Language"
                },
                new Tag
                {
                    Id = 4,
                    Name = ".NET"
                },
                new Tag
                {
                    Id = 5,
                    Name = "Python"
                },
                new Tag
                {
                    Id = 6,
                    Name = "JavaScript"
                }
                );
        }
    }
}
