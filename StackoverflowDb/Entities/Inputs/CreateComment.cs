namespace StackoverflowDb.Entities.Inputs;

public class CreateComment
{
    public required int Id { get; set; }
    public required string Message { get; set; }
}