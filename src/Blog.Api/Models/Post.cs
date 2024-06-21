using Blog.Api.Services;

namespace Blog.Api.Models;

public record Post
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }

    public User? Author { get; set; }
}