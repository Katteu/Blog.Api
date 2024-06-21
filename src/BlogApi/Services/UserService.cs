using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.DTO;

namespace Blog.Api.Services;

public class UserServices : BaseService<User, UserRequest, UserResponse>
{
    public UserServices(Database<User> database) : base(database)
    {
    }

    public override IResult Create(UserRequest request)
    {
        if (_database.Data.Any(u => u.Username == request.Username))
        {
            return Results.Conflict("Username is already taken.");
        }
        return base.Create(request);
    }

    public IResult Update(UserRequest request, int id)
    {
        var existingUser = _database.Data.FirstOrDefault(u => u.Id == id);

        if (_database.Data.Any(u => u.Username == request.Username))
        {
            return Results.Conflict("Username is already taken.");
        }

        if (existingUser is null)
        {
            return Results.NotFound("User does not exist.");

        }

        existingUser.Username = request.Username;
        existingUser.Password = request.Password;
        return Results.Ok(new UserResponse(existingUser.Id, existingUser.Username));
    }

    public IResult GetByUsername(string username)
    {
        var user = _database.Data.FirstOrDefault(u => u.Username == username);

        if (user is not null)
        {
            return Results.Ok(new UserResponse(user.Id, user.Username));
        }
        else
        {
            return Results.NotFound("User cannot be found.");
        }
    }

    protected override UserResponse MapToResponse(User model)
    {
        return new UserResponse(model.Id, model.Username);
    }


    protected override User MapToModel(UserRequest request, User? existingModel = default)
    {
        return existingModel ?? new User { Username = request.Username, Password = request.Password };
    }
}

