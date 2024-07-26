using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.Entities;
using System.Text.Json.Serialization;
using StackoverflowDb.Entities.Inputs;
using StackoverflowDb;
using StackoverflowDb.Entities.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using StackoverflowDb.Entities.Errors;

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
var testUserGuid = "32DEF023-90BF-4109-F8DD-08DCABF66FAA";

app.MapPost("/add-question", async (StackoverflowDbContext db,
    [AsParameters] CreateQuestion newQuestion) =>
{
    var testUser = await StackoverflowOperations.GetTestUserAsync(db, testUserGuid);
    var question = StackoverflowOperations.CreateQuestion(newQuestion, testUser);

    try
    {
        await db.Questions.AddAsync(question);
        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {
        var error = new Error
        {
            ErrorMessage = e.Message
        };
        return Results.Problem(error.ToString());
    }

    var response = new QuestionDto
    {
        Id = question.Id,
        Title = question.Title,
        Content = question.Content,
        PublicationDate = question.PublicationDate,
        AuthorId = question.Author.Id,
    };

    return Results.Ok(response);
});// Add Question
app.MapPut("/comment-question", async (StackoverflowDbContext db,
    [AsParameters] CreateComment newComment) =>
{
    var question = await db.Questions.FirstOrDefaultAsync(q => q.Id == newComment.Id);

    if (question is null)
    {
        return Results.NotFound($"Question with ID = {newComment.Id} does not exist.");
    }
    var testUser = await StackoverflowOperations.GetTestUserAsync(db, testUserGuid);

    var comment = StackoverflowOperations.CreateCommentForQuestion(newComment, question, testUser);

    try
    {
        await db.QuestionComments.AddAsync(comment);
        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {
        var error = new Error
        {
            ErrorMessage = e.Message
        };
        return Results.Problem(error.ToString());
    }

    var response = new CommentDto
    {
        Message = comment.Message,
        BelongToId = comment.QuestionId,
        AuthorId = comment.Author.Id,
        PublicationDate = comment.PublicationDate,
    };

    return Results.Ok(response);
});// Comment Question
app.MapPost("/answer-question", async (StackoverflowDbContext db,
    [AsParameters] CreateAnswer newAnswer) =>
{
    var testUser = await StackoverflowOperations.GetTestUserAsync(db, testUserGuid);

    var question = await db.Questions
        .FirstOrDefaultAsync(q => q.Id == newAnswer.QuestionId);

    if (question is null)
    {
        return Results.NotFound($"No question with ID number {newAnswer.QuestionId} in database.");
    }

    var answer = StackoverflowOperations.CreateAnswer(newAnswer, question, testUser);

    try
    {
        await db.Answers.AddAsync(answer);
        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {
        var error = new Error
        {
            ErrorMessage = e.Message
        };
        return Results.Problem(error.ToString());
    }

    var response = new AnswerDto
    {
        Message = answer.Message,
        QuestionId = answer.QuestionId,
        AuthorId = answer.Author.Id,
        PublicationDate = answer.PublicationDate,
    };

    return Results.Ok(response);
});// Add Answer
app.MapPut("/comment-answer", async (StackoverflowDbContext db,
    [AsParameters] CreateComment newComment) =>
{
    var answer = await db.Answers.FirstOrDefaultAsync(q => q.Id == newComment.Id);

    if (answer is null)
    {
        return Results.NotFound($"Answer with ID = {newComment.Id} does not exist.");
    }
    var testUser = await StackoverflowOperations.GetTestUserAsync(db, testUserGuid);

    var comment = StackoverflowOperations.CreateCommentForAnswer(newComment, answer, testUser);

    try
    {
        await db.AnswerComments.AddAsync(comment);
        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {
        var error = new Error
        {
            ErrorMessage = e.Message
        };
        return Results.Problem(error.ToString());
    }

    var response = new CommentDto
    {
        Message = comment.Message,
        BelongToId = comment.AnswerId,
        AuthorId = comment.Author.Id,
        PublicationDate = comment.PublicationDate,
    };

    return Results.Ok(response);
});// Comment Question
app.MapGet("/get-questions", 
    async (StackoverflowDbContext db) =>
{
    var questions = await db.Questions
        .Include(q => q.Tags)
        .Select(q => new QuestionDto
        {
            Id = q.Id,
            Title = q.Title,
            Content = q.Content,
            AuthorId = q.AuthorId,
            PublicationDate = q.PublicationDate,
            Tags = StackoverflowOperations.ConvertToTagDto(q.Tags)
                .ToList(),
        })
        .ToListAsync();

    return questions;
});// Get Questions
app.MapGet("/get-answers", 
    async (StackoverflowDbContext db) =>
{
    var answers = await db.Answers
        .Select(a => new AnswerDto
        {
            Id = a.Id,
            Message = a.Message,
            QuestionId = a.QuestionId,
            AuthorId = a.AuthorId,
            PublicationDate = a.PublicationDate
        })
        .ToListAsync();

    return answers;
});// Get Answers
app.MapPut("/add-tag-to-question", async (StackoverflowDbContext db,
    [AsParameters] AddTagToQuestion parameters) =>
{
    var tag = await db.Tags.FirstOrDefaultAsync(t => t.Id == parameters.TagId);
    var question = await db.Questions.FirstOrDefaultAsync(q => q.Id == parameters.QuestionId);

    if(tag is null)
    {
        return Results.NotFound($"Tag with ID = {parameters.TagId} does not exist.");
    }
    if(question is null)
    {
        var error = new Error
        {
            ErrorMessage = $"Question with ID = {parameters.QuestionId} does not exist."
        };
        return Results.NotFound(error);
    }

    try
    {
        question.Tags.Add(tag);
        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {
        var error = new Error 
        { 
            ErrorMessage = e.Message,
        };
        return Results.Problem(error.ToString());
    }

    var response = db.Questions
        .Include(q => q.Tags)
        .Select(q => new QuestionDto
        {
            Id = q.Id,
            Title = q.Title,
            Content = q.Content,
            AuthorId = q.AuthorId,
            PublicationDate = q.PublicationDate,
            Tags = q.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList(),
        })
        .First(q => q.Id == parameters.QuestionId);

    return Results.Ok(response);
});
app.MapGet("/get-tags", async (StackoverflowDbContext db) =>
{
    var tags = await db.Tags
        .Select(t => new TagDto
        {
            Id = t.Id,
            Name = t.Name,
        })
        .ToListAsync();

    return tags;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetService<StackoverflowDbContext>()!;



app.UseHttpsRedirection();

app.Run();