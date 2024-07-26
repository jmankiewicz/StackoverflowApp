using System.Text.Json.Serialization;

namespace StackoverflowDb.Entities;

public class Answer
{
    public int Id { get; set; }
    public int Votes { get; set; }
    public required string Message { get; set; }
    public DateTime PublicationDate { get; set; }

    public User Author { get; set; }
    public Guid AuthorId { get; set; }

    public Question Question { get; set; }
    public int QuestionId { get; set; }

    public List<AnswerComment> Comments { get; set; } = [];
}