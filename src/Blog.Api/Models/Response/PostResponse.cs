namespace Blog.Api.Models.Response;

// Used when retrieving post information
public record class PostResponse(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    int AuthorId
);
