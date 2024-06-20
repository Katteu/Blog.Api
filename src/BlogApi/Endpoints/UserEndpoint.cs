using UserModel;
using BaseEndpoints;
namespace UserEndpoints;

// User-specific endpoint that inherits from BaseEndpoint<User>
public class UserEndpoint : BaseEndpoint<User>
{
    // User-specific logic and overrides

    public UserEndpoint() : base()
    {
    }

    // Add a new method to retrieve a user by username
    public IResult GetUserByUsername(string username)
    {
        // Find a user with the matching username
        User? user = _entities.Find(user => user.Username == username);
        // Return a 404 error if the user is not found
        return user == null ? Results.NotFound("Username cannot be found") : Results.Ok(user);
    }

    // Update an existing user
    public IResult UpdateUser(User updatedUser, int id)
    {
        // Get the user to be updated
        User? user = base.Get(id);

        if (user is null)
        {
            // Return a 404 error if the user to be edited does not exist
            return Results.NotFound("The user to be edited does not exist.");
        }

        // Check if the updated username is already taken
        if (_entities.Find(u => u.Username == updatedUser?.Username) is not null)
        {
            // Return a 409 error if the username is already taken
            return Results.Conflict("Username is already taken.");
        }

        // Update the user's properties
        user.Username = updatedUser.Username;
        user.Password = updatedUser.Password;

        // Return a 200 OK response with the updated user
        return Results.Ok(user);
    }

    // Override CreateUser to include validation or additional logic
    public override IResult Create(User user)
    {
        // Check if the username is already taken
        if (_entities.Find(u => u.Username == user.Username) is not null)
        {
            // Return a 409 error if the username is already taken
            return Results.Conflict("Username is already taken.");
        }

        // Call the base Create method to create the user
        return base.Create(user);
    }
}