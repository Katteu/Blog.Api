using BlogModel;
using UserEndpoints;
using BaseEndpoints;
using Microsoft.AspNetCore.Http;


namespace BlogEndpoints;
public class BlogEndpoint : BaseEndpoint<Blog>
{

    public BlogEndpoint() : base() { }
    

    public IResult GetPostByAuthor(int authorId)
    {
        List<Blog> posts = _entities.FindAll(post => post.AuthorId == authorId);
        return !posts.Any() ? Results.Ok("No posts found for this author.") : Results.Ok(posts);
    }

    public IResult UpdateBlog(Blog updatedPost, int id)
    {
        Blog? post = base.Get(id);

        if(post is null)
        {
            return Results.NotFound("The post to be edited does not exist.");
        }

        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;

        return Results.Ok(post);
    }

    public override IResult Create(Blog post)
    {
        post.CreatedAt = DateTime.Now;
        return base.Create(post);
    }
}