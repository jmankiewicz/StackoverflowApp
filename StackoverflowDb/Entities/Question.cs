using System.Text.Json.Serialization;

namespace StackoverflowDb.Entities;

public class Question
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime PublicationDate { get; set; }

    [JsonIgnore]
    public User Author { get; set; }
    public Guid AuthorId { get; set; }

    public List<QuestionComment> Comments { get; set; } = [];
    public List<Answer> Answers { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
}
