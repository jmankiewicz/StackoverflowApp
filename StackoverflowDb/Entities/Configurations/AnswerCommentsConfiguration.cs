using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoverflowDb.Entities.Configurations
{
    public class AnswerCommentsConfiguration : IEntityTypeConfiguration<AnswerComment>
    {
        public void Configure(EntityTypeBuilder<AnswerComment> builder)
        {
            builder.Property(c => c.PublicationDate).HasDefaultValueSql("getutcdate()");
        }
    }
}