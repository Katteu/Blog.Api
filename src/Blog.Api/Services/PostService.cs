using Blog.Api.Models;
using Blog.Api.Models.Request;
using Blog.Api.Models.Response;
using Blog.Api.Data;
using Blog.Api.Services;
namespace Blog.Api.Services;
public class PostServices
{
    protected readonly AppDbContext _dbContext;

    public PostServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IResult GetAllPosts()
    {
        var posts = _dbContext.Posts
            .Select(MapToResponse)
            .ToList();
        return Results.Ok(posts);
    }

    public IResult GetById(int id)
    {
        var model = _dbContext.Posts.SingleOrDefault(e => e.Id == id);
        return model is not null ? Results.Ok(MapToResponse(model)) : Results.NotFound("Entity or record not found.");
    }

    public IResult CreatePost(PostRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.AuthorId);

        if (user == null)
        {
            return Results.NotFound("User not found.");
        }

        var model = MapToModel(request);
        _dbContext.Posts.Add(model);
        _dbContext.SaveChanges();
        return Results.Ok(MapToResponse(model));
    }

    public IResult UpdatePost(PostRequest request, int id)
    {
        var existingPost = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

        if (existingPost is null)
        {
            return Results.NotFound("Post not found.");
        }

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.AuthorId);

        if (user is null)
        {
            return Results.NotFound("Author not found.");
        }

        existingPost.Title = request.Title;
        existingPost.Content = request.Content;
        
        _dbContext.SaveChanges();
        return Results.Ok(MapToResponse(existingPost));
    }

    public IResult DeletePost(int id)
    {
        var model = _dbContext.Posts.SingleOrDefault(e => e.Id == id);

        if (model is null)
        {
            return Results.NotFound("Entity or record cannot be found.");
        }

        _dbContext.Posts.Remove(model);
        _dbContext.SaveChanges();
        
        return Results.Ok(MapToResponse(model));
    }

    public IResult GetPostsByAuthor(int authorId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == authorId);

        if (user == null)
        {
            return Results.NotFound("User not found.");
        }

        if (user.Posts == null || !user.Posts.Any())
        {
            return Results.Ok("No posts exist under this Author.");
        }

        var posts = user.Posts
            .Select(MapToResponse)
            .ToList();

        return Results.Ok(posts);
    }

    protected PostResponse MapToResponse(Post model)
    {
        return new PostResponse(model.Id, model.Title, model.Content, model.CreatedAt, model.AuthorId);
    }

    protected Post MapToModel(PostRequest request)
    {
        return new Post
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = request.AuthorId,
            CreatedAt = DateTime.UtcNow
        };
    }
}