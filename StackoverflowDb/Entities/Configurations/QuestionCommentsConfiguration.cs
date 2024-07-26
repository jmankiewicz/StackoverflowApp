using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoverflowDb.Entities.Configurations
{
    public class QuestionCommentsConfiguration : IEntityTypeConfiguration<QuestionComment>
    {
        public void Configure(EntityTypeBuilder<QuestionComment> builder)
        {
            builder.Property(c => c.PublicationDate).HasDefaultValueSql("getutcdate()");
        }
    }
}
