using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.Entities;
using StackoverflowDb.Entities.Inputs;
using System.ComponentModel;
using System.Text.Json.Serialization;
using StackoverflowDb.Entities.DTO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StackoverflowDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StackoverflowConnectionString"));
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetService<StackoverflowDbContext>()!;
var users = dbContext.Users.ToList();

app.MapPost("/add-user", async(StackoverflowDbContext db, 
    [AsParameters] AddUser addUser) =>
{
    var user = new User
    {
        Nickname = addUser.Nickname,
        Email = addUser.Email,
    };

    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();

    return user;
});

app.MapPost("/ask-a-question", async(StackoverflowDbContext db,
    [AsParameters] AddQuestion addQuestion) =>
{
    // Test user, available for adding questions
    var author = db.Users
        .First(u => u.Nickname == "John_Doe");

    var question = new Question
    {
        Title = addQuestion.Title,
        Content = addQuestion.Content,
        Author = author,
    };

    await db.Questions.AddAsync(question);

    await db.SaveChangesAsync();

    return new QuestionDto
    {
        Id = question.Id,
        Title = question.Title,
        Content = question.Content,
        PublicationDate = question.PublicationDate,
        Author = author.Nickname,
    };
});

app.MapGet("/get-user-details", 
    async (StackoverflowDbContext db) =>
{

    var johnDoe = await db.Users
        .Include(u => u.Questions)
        .Include(u => u.Answers)
        .Include(u => u.Comments)
        .FirstAsync(u => u.Nickname == "John_Doe");

    return new UserDetailsDto
    {
        Id = johnDoe.Id,
        Nickname = johnDoe.Nickname,
        Email = johnDoe.Email,
        NumberOfQuestions = johnDoe.Questions.Count,
        NumberOfAnswers = johnDoe.Answers.Count,
        NumberOfComments = johnDoe.Comments.Count,
    };
});

app.MapGet("/get-user-questions",
    async (StackoverflowDbContext db) =>
{
    var userQuestions = db.Questions
        .Include(q => q.Author)
        .Where(q => q.Author.Nickname == "John_Doe")
        .Select(q => new
        {
            q.Author.Nickname,
            q.Title,
            q.Content,
            q.PublicationDate,
        })
        .ToList();

    return userQuestions;
});

app.MapDelete("/delete-question", async (StackoverflowDbContext db, [FromQuery] string title) =>
{
    QuestionDto result;

    try
    {
        result = await StackoverflowOperations.DeleteQuestionAsync(db, title);

    }
    catch (Exception ex)
    {
        return Results.NotFound(title);
    }


    return Results.Json(result);
});

app.UseHttpsRedirection();

app.Run();

public class StackoverflowOperations
{
    public static async Task<QuestionDto> DeleteQuestionAsync(StackoverflowDbContext db, string title)
    {
        var question = await db.Questions.FirstOrDefaultAsync(q => q.Title == title);
        var result = await db.Questions
            .Where(q => q.Title == title)
            .Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Author = q.Author.Nickname,
                Content = q.Content,
                PublicationDate = q.PublicationDate,
            })
            .FirstOrDefaultAsync();

        db.Questions.Remove(question);
        db.SaveChanges();

        return result;
    }
}
