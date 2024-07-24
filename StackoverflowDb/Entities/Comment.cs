namespace StackoverflowDb.Entities;

public class Comment
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public DateTime PublicationDate { get; set; }

    public User Author { get; set; }
    public Guid AuthorId { get; set; }

    public Question? Question { get; set; }
    public int QuestionId { get; set; }

    public Answer? Answer { get; set; }
    public int AnswerId { get; set; }
}
