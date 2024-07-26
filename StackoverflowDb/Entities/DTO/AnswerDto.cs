namespace StackoverflowDb.Entities.DTO
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public int QuestionId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
