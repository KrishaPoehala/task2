﻿namespace task2.Common.Dtos;

public record ReviewDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Reviewer { get; set; }
}
