namespace Blog.Api.Models.DTO;

// sabton sa nato nganong record class T_T pwede pud ta mag add annotations
// Used when creating or updating a post
public record class PostRequest(
    string Title,
    string Content,
    int AuthorId
);

// Used when retrieving post information
public record class PostResponse(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    int AuthorId
);