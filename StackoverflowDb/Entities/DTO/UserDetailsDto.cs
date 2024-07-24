namespace StackoverflowDb.Entities.DTO
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfAnswers { get; set; }
        public int NumberOfComments { get; set; }
    }
}
