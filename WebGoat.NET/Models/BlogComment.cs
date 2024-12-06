using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.Models;

public class BlogComment
{
    [Key]
    public int Id { get; init; }
    
    public required int PostId { get; init; }

    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    
    public required string AuthorName { get; init; }
}