using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.Request;
using Blog.Api.Models.Response;
using Blog.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Services;

public class UserServices
{
    protected readonly AppDbContext _dbContext;

    public UserServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IResult GetAllUsers()
    {
        var users = _dbContext.Users
            .Select(MapToResponse)
            .ToList();

        return Results.Ok(users);
    }

    public IResult GetById(int id)
    {
        var user = _dbContext.Users.SingleOrDefault(e => e.Id == id);

        return user is not null ? Results.Ok(MapToResponse(user)) : Results.NotFound("Entity or record not found.");
    }

    public IResult CreateUser(UserRequest request)
    {
        if (_dbContext.Users.Any(u => u.Username == request.Username))
        {
            return Results.Conflict("Username is already taken.");
        }

        var user = MapToModel(request);
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Results.Ok(MapToResponse(user));
    }

    public IResult DeleteUser(int id)
    {
        var user = _dbContext.Users.SingleOrDefault(e => e.Id == id);

        if (user is null)
        {
            return Results.NotFound("Entity or record cannot be found.");
        }

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return Results.Ok(MapToResponse(user));
    }

    public IResult UpdateUser(UserRequest request, int id)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);

        if (_dbContext.Users.Any(u => u.Username == request.Username && u.Id != id))
        {
            return Results.Conflict("Username is already taken.");
        }

        if (existingUser is null)
        {
            return Results.NotFound("User does not exist.");
        }

        existingUser.Username = request.Username;
        existingUser.Password = request.Password;

        _dbContext.Users.Update(existingUser);
        _dbContext.SaveChanges();

        return Results.Ok(MapToResponse(existingUser));
    }

    public IResult GetByUsername(string username)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

        if (user is not null)
        {
            return Results.Ok(MapToResponse(user));
        }
        else
        {
            return Results.NotFound("User cannot be found.");
        }
    }

    protected UserResponse MapToResponse(User model)
    {
        return new UserResponse(model.Id, model.Username);
    }

    protected User MapToModel(UserRequest request, User? existingModel = null)
    {
        return existingModel ?? new User
        {
            Username = request.Username,
            Password = request.Password
        };
    }
}