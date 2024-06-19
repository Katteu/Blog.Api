using BlogModel;
using Microsoft.AspNetCore.Http;

namespace BlogEndpoint;
public class BlogEP
{

    public static List<Blog> Posts = new List<Blog>();

    public static List<Blog> GetAllPosts()
    {
        return Posts;
    }

    public static IResult GetPost(int id)
    {
        Blog post = Posts.Find(post => post.Id == id);

        if (post is null)
        {
            return Results.NotFound("This post does not exist.");
        }

        return Results.Ok(post);
    }

    public static IResult GetPostByAuthor(int id)
    {
        List<Blog> posts = Posts.FindAll(post => post.AuthorId == id);

        if (posts.Count == 0)
        {
            return Results.NotFound("No posts exist under this Author.");
        }

        return Results.Ok(posts);
    }

    public static Blog CreatePost(Blog post)
    {
        int currId = Posts.Count == 0 ? 0 : Posts.Max(post => post.Id);
        post.Id = currId + 1;

        Posts.Add(post);
        return post;
    }

    public static IResult UpdatePost(Blog updatedPost, int id)
    {
        Blog post = Posts.Find(post => post.Id == id);

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
        Blog post = Posts.Find(post => post.Id == id);

        if (post is null)
        {
            return Results.NotFound("The post to be removed does not exist.");
        }

        Posts.Remove(post);
        return Results.Ok(post);
    }

}