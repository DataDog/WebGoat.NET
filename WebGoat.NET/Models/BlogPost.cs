namespace WebGoat.NET.Models;

public class BlogPost
{
    public required int Id { get; init; }
    
    public required string Title { get; init; }
    
    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }

    public required string AuthorName { get; init; }
}