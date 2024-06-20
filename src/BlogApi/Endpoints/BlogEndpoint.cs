using BlogModel;
using UserEndpoints;
using BaseEndpoints;
using Microsoft.AspNetCore.Http;

namespace BlogEndpoints;

// BlogEndpoint class that inherits from BaseEndpoint<Blog>
public class BlogEndpoint : BaseEndpoint<Blog>
{
    // Constructor that calls the base constructor
    public BlogEndpoint() : base() { }

    // Method to get blog posts by author Id
    public IResult GetPostByAuthor(int authorId)
    {
        // Find all blog posts with the matching author Id
        List<Blog> posts = _entities.FindAll(post => post.AuthorId == authorId);
        // Return a 200 OK response with a message if no posts are found
        return !posts.Any() ? Results.Ok("No posts found for this author.") : Results.Ok(posts);
    }

    // Method to update an existing blog post
    public IResult UpdateBlog(Blog updatedPost, int id)
    {
        // Get the blog post to be updated
        Blog? post = base.Get(id);

        if (post is null)
        {
            // Return a 404 error if the post to be edited does not exist
            return Results.NotFound("The post to be edited does not exist.");
        }

        // Update the blog post's properties
        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;

        // Return a 200 OK response with the updated post
        return Results.Ok(post);
    }

    // Override the Create method to set the CreatedAt timestamp
    public override IResult Create(Blog post)
    {
        // Set the CreatedAt timestamp to the current datetime
        post.CreatedAt = DateTime.Now;
        // Call the base Create method to create the blog post
        return base.Create(post);
    }
}