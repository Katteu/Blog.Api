using Blog.Api.Services;

namespace Blog.Api.Models;

public record User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public ICollection<Post>? Posts { get; set; }
}