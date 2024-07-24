namespace StackoverflowDb.Entities.Inputs
{
    public class AddUser
    {
        public required string Nickname { get; set; }
        public required string Email { get; set; }
    }
}
