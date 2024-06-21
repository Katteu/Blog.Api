using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.Request;
using Blog.Api.Services;
namespace Blog.Api.Endpoints;

public class UserEndpoint : IEndpointModule
{

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users")
                       .WithOpenApi()
                       .WithTags("Users");

        group.MapGet("/", (UserServices userService) => userService.GetAllUsers());
        group.MapGet("/{id}", (UserServices userService, int id) => userService.GetById(id));
        group.MapGet("/username/{username}",(UserServices userService,string username) => userService.GetByUsername(username));
        group.MapPost("/", (UserServices userService,UserRequest request) => userService.CreateUser(request));
        group.MapPut("/{id}", (UserServices userService,UserRequest req, int id) => userService.UpdateUser(req, id));
        group.MapDelete("/{id}",(UserServices userService, int id) => userService.DeleteUser(id));
    }
}