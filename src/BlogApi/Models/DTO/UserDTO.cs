namespace Blog.Api.Models.DTO;

// Used when creating or updating a user
public record class UserRequest(
    string Username,
    string Password
);

// Used when retrieving user information
public record class UserResponse(
    int Id,
    string Username
);