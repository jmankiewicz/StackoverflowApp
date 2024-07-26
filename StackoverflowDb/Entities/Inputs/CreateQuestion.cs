namespace StackoverflowDb.Entities.Inputs
{
    public class CreateQuestion
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}
