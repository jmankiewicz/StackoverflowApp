namespace StackoverflowDb.Entities.DTO
{
    public class CommentDto
    {
        public required string Message { get; set; }
        public int BelongToId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
