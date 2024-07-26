using Microsoft.EntityFrameworkCore;
using StackoverflowDb.Entities;
using StackoverflowDb.Entities.DTO;
using StackoverflowDb.Entities.Inputs;
using System.Reflection;

namespace StackoverflowDb;

public class StackoverflowOperations
{
    /// <summary>
    /// Asynchronously returns object of user named 'John_Doe' for testing stackoverflowApp
    /// </summary>
    /// <param name="db"></param>
    /// <returns>
    /// The task that represents the asynchronous operation.
    /// The task result contains the object of user with GUID = <paramref name="userId"/>
    /// </returns>
    public static async Task<User> GetTestUserAsync(StackoverflowDbContext db, string userId)
        => await db.Users
            .FirstAsync(u => u.Id == Guid.Parse(userId));

    /// <summary>
    /// Create an instance of 'Question' class
    /// </summary>
    /// <param name="newQuestion"></param>
    /// <param name="questionAuthor"></param>
    /// <returns>
    /// The 'CreateQuestion' method returns instance of 'Question' class which contains user inputs from <paramref name="newQuestion"/>
    /// </returns>
    public static Question CreateQuestion(CreateQuestion newQuestion, User questionAuthor)
        => new() { Title = newQuestion.Title, Content = newQuestion.Content, Author = questionAuthor };

    /// <summary>
    /// Create an instance of 'Answer' class
    /// </summary>
    /// <param name="newAnswer"></param>
    /// <param name="answerAuthor"></param>
    /// <returns>
    /// The 'CreateAnswer' method returns instance of 'Answer' class which contains user input from <paramref name="newAnswer"/>
    /// </returns>
    public static Answer CreateAnswer(CreateAnswer newAnswer, Question question, User answerAuthor)
        => new() { Question = question, Message = newAnswer.Message, Author = answerAuthor };

    public static IEnumerable<TagDto> ConvertToTagDto(IEnumerable<Tag> tags)
        => tags.Select(t => new TagDto { Id = t.Id, Name = t.Name });

    public static QuestionComment CreateCommentForQuestion(CreateComment newComment, Question question, User commentAuthor)
        => new() { Question = question, Message = newComment.Message, Author = commentAuthor };

    public static AnswerComment CreateCommentForAnswer(CreateComment newComment, Answer answer, User commentAuthor)
        => new() { Answer = answer, Message = newComment.Message, Author = commentAuthor };
}