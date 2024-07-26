namespace StackoverflowDb.Entities.Inputs
{
    public class AddTagToQuestion
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
    }
}
