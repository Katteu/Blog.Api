namespace Blog.Api.Models.Response;

// Used when retrieving user information
public record class UserResponse(
    int Id,
    string Username
);