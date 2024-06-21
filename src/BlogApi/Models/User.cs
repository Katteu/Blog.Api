using Blog.Api.Services;

namespace Blog.Api.Models;

public record User : IEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

}                                                       