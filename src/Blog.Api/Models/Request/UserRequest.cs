namespace Blog.Api.Models.Request;

// Used when creating or updating a user
public record class UserRequest(
    string Username,
    string Password
);