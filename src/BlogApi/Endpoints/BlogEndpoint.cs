using BlogModel;
using UserEndpoints;
using Microsoft.AspNetCore.Http;


namespace BlogEndpoints;
public class BlogEndpoint
{

    public static List<Blog> Posts = new List<Blog>();

    public static List<Blog> GetAllPosts()
    {
        return Posts;
    }

    public static IResult GetPost(int id)
    {
        Blog? post = Posts.Find(post => post.Id == id);

        if (post is null)
        {
            return Results.NotFound("This post does not exist.");
        }

        return Results.Ok(post);
    }

    public static IResult GetPostByAuthor(int id)
    {
        List<Blog> posts = Posts.FindAll(post => post.AuthorId == id);

        if(UserEndpoint.Users.Find(user => user.Id == id) is null)
        {
            return Results.NotFound("Author does not exist.");
        }

        return !posts.Any() ? Results.Ok("No posts exist under this Author.") : Results.Ok(posts);
    }

    public static Blog CreatePost(Blog post)
    {
        int currId = !Posts.Any() ? 0 : Posts.Max(post => post.Id);
        post.Id = currId + 1;
        post.CreatedAt = DateTime.Now;
        Posts.Add(post);
        return post;
    }

    public static IResult UpdatePost(Blog updatedPost, int id)
    {
        Blog? post = Posts.Find(post => post.Id == id);

        if (post is null)
        {
            return Results.NotFound("The post to be edited does not exist.");
        }

        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;

        return Results.Ok(post);
    }

    public static IResult DeletePost(int id)
    {
        Blog? post = Posts.Find(post => post.Id == id);

        if (post is null)
        {
            return Results.NotFound("The post to be removed does not exist.");
        }

        Posts.Remove(post);
        return Results.Ok(post);
    }

}