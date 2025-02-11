﻿namespace StackoverflowDb.Entities;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Question> Questions { get; set; } = [];
}
