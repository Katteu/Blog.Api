using UserModel;
using BaseEndpoints;
namespace UserEndpoints;

public class UserEndpoint : BaseEndpoint<User>
{
    // User-specific logic and overrides

    public UserEndpoint() : base()
    {
    }

    // Add a new method to retrieve a user by username
    public IResult GetUserByUsername(string username)
    {
        User? user = _entities.Find(user => user.Username == username);
        return user == null ? Results.NotFound() : Results.Ok(user);
    }
    public IResult UpdateUser(User updatedUser, int id)
    {
        User? user = base.Get(id);

        if(user is null)
        {
            return Results.NotFound("The user to be edited does not exist.");
        }

        if (_entities.Find(u => u.Username == updatedUser?.Username) is not null)
        {
            return Results.Conflict("Username is already taken.");
        }

        user.Username = updatedUser.Username;
        user.Password = updatedUser.Password;

        return Results.Ok(user);
    }

    // Override CreateUser to include validation or additional logic
    public override IResult Create(User user)
    {
        if (_entities.Find(u => u.Username == user.Username) is not null)
        {
            return Results.Conflict("Username is already taken.");
        }

        return base.Create(user);
    }
}