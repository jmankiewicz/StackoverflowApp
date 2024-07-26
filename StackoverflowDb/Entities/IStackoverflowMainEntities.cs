namespace StackoverflowDb.Entities;

public interface IStackoverflowMainEntities
{
    public int Id { get; set; }
    public string Content { get; set; }
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime PublicationDate { get; set; }
}