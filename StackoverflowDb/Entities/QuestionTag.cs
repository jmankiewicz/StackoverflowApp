namespace StackoverflowDb.Entities;

public class QuestionTag
{
    public virtual required Question Question { get; set; }
    public int QuestionId { get; set; }

    public virtual required Tag Tag { get; set; }
    public int TagId { get; set; }
}
