using System.Text.Json.Serialization;

namespace StackoverflowDb.Entities.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PublicationDate { get; set; }

        public Guid AuthorId { get; set; }

        public List<TagDto> Tags { get; set; } = [];
    }
}
