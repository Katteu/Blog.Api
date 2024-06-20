using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.DTO;

public static class UserEndpoints
{
    /// <summary>
    /// Maps User endpoints to the WebApplication.
    /// </summary>
    public static void MapUserEndpoints(this WebApplication app)
    {
        // Get All Users*
        app.MapGet("/user", (Database database) =>{
           return database.Users.Select(u => new UserResponse(u.Id, u.Username));
        }).WithDisplayName("Get All Users").WithTags("User");

        // Get User by Id*
        app.MapGet("/user/{id}", (Database database, int id) =>
        {
            var user = database.Users.FirstOrDefault(u => u.Id == id);

            if (user is not null)
            {
                return Results.Ok(new UserResponse(user.Id, user.Username));
            }
            else
            {
                return Results.NotFound("User cannot be found.");
            }
        }).WithDisplayName("Get User by Id").WithTags("User");

        // Get User by Username*
        app.MapGet("/user/username/{username}", (Database database, string username) =>
        {
            var user = database.Users.FirstOrDefault(u => u.Username == username);

            if (user is not null)
            {
                return Results.Ok(new UserResponse(user.Id, user.Username));
            }
            else
            {
                return Results.NotFound("User cannot be found.");
            }
        }).WithDisplayName("Get User by Username").WithTags("User");


        // Create User*
        app.MapPost("/user", (Database database, UserRequest userRequest) =>
        {
            int currId = database.Users.Count == 0 ? 1 : database.Users.Max(user => user.Id) + 1;

            if (database.Users.Any(u => u.Username == userRequest.Username))
            {
                return Results.Conflict("Username is already taken.");
            }

            var user = new User { Id = currId, Username = userRequest.Username, Password = userRequest.Password };

            database.Users.Add(user);

            return Results.Created($"/user/{user.Id}", new UserResponse(user.Id, user.Username));
        }).WithDisplayName("Create User").WithTags("User");

        // Update User*
        app.MapPut("/user/{id}", (Database database, UserRequest userRequest, int id) =>
        {
            var existingUser = database.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser is not null)
            {
                existingUser.Username = userRequest.Username;
                existingUser.Password = userRequest.Password;
                return Results.Ok(new UserResponse(existingUser.Id, existingUser.Username));
            }
            else
            {
                return Results.NotFound("User does not exist.");
            }
        }).WithDisplayName("Update User").WithTags("User");

        // Delete User*
        app.MapDelete("/user/{id}", (Database database, int id) =>
        {
            var user = database.Users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                database.Users.Remove(user);
                return Results.Ok();
            }
            else
            {
                return Results.NotFound("User does not exist.");
            }
        }).WithDisplayName("Delete User").WithTags("User");
    }
}