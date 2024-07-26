namespace StackoverflowDb.Entities.Inputs
{
    public class CreateAnswer
    {
        public required int QuestionId { get; set; }
        public required string Message { get; set; }
    }
}
