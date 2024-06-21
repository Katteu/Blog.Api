using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.DTO;

namespace Blog.Api.Services;

public class PostServices : BaseService<Post, PostRequest, PostResponse>
{
    public PostServices(Database<Post> database) : base(database)
    {
    }

    public IResult Update(PostRequest request, int id)
    {
        var existingPost = _database.Data.FirstOrDefault(p => p.Id == id);
        if (existingPost is not null)
        {
            existingPost.Title = request.Title;
            existingPost.Content = request.Content;
            existingPost.AuthorId = request.AuthorId;
            return Results.Ok(new PostResponse(existingPost.Id, existingPost.Title, existingPost.Content, existingPost.CreatedAt, existingPost.AuthorId));
        }
        else
        {
            return Results.NotFound("Post not found.");
        }
    }

    public IResult GetPostsByAuthor(int authorId)
    {
        IEnumerable<PostResponse> posts = _database.Data
                .Where(p => p.AuthorId == authorId)
                .Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt, p.AuthorId))
                .ToList();

        return !posts.Any() ? Results.Ok("No posts exist under this Author.") : Results.Ok(posts);
    }

    protected override PostResponse MapToResponse(Post model)
    {
        return new PostResponse(model.Id, model.Title, model.Content, model.CreatedAt, model.AuthorId);
    }

    protected override Post MapToModel(PostRequest request, Post? existingModel = default)
    {
        return existingModel ?? new Post { Title = request.Title, Content = request.Content, AuthorId = request.AuthorId, CreatedAt = DateTime.Now };
    }
}

