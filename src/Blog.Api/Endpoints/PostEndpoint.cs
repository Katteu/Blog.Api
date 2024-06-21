using System.Data.Common;
using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.Request;
using Blog.Api.Services;

namespace Blog.Api.Endpoints;

public class PostEndpoint : IEndpointModule
{

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/posts")
                       .WithOpenApi()
                       .WithTags("Posts");

        group.MapGet("/", (PostServices postService) => postService.GetAllPosts());
        group.MapGet("/{id}", (PostServices postService, int id) =>  postService.GetById(id));
        group.MapGet("/author/{authorId}",(PostServices postService,int authorId) => postService.GetPostsByAuthor(authorId));
        group.MapPost("/", (PostServices postService, PostRequest postRequest) => postService.CreatePost(postRequest));
        group.MapPut("/{id}", (PostServices postService, PostRequest postRequest, int id) => postService.UpdatePost(postRequest, id));
        group.MapDelete("/{id}", (PostServices postService, int id) => postService.DeletePost(id));
    }
}