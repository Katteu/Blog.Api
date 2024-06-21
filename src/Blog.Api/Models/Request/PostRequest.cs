namespace Blog.Api.Models.Request;

// Used when creating or updating a post
public record class PostRequest(
    string Title,
    string Content,
    int AuthorId
);