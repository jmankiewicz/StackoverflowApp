using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StackoverflowDb.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required string Email { get; set; }

    public List<Question> Questions { get; set; } = [];
    public List<Answer> Answers { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
}
