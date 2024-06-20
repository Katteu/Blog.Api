using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Models.DTO;

public static class PostEndpoints
{
    /// <summary>
    /// Maps post endpoints to the WebApplication
    /// </summary>
    public static void MapPostEndpoints(this WebApplication app)
    {
        // Get all posts*
        app.MapGet("/post", (Database database) => {
            return database.Posts.Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt, p.AuthorId));
        }).WithDisplayName("Get All Posts").WithTags("Blog Post");

        // Get a post by ID*
        app.MapGet("/post/{id}", (Database database, int id) =>
        {
            Post? post = database.Posts.FirstOrDefault(p => p.Id == id);

            if (post is not null)
            {
                return Results.Ok(new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt, post.AuthorId));
            }
            else
            {
                return Results.NotFound("This post does not exist.");
            }
        }).WithDisplayName("Get Post by Id").WithTags("Blog Post");

        // Get posts by author ID*
        app.MapGet("/post/author/{Authorid}", (Database database, int Authorid) =>
        {

            if (database.Users.Find(user => user.Id == Authorid) is null)
            {
                return Results.NotFound("Author does not exist.");
            }

            IEnumerable<PostResponse> posts = database.Posts
                    .Where(p => p.AuthorId == Authorid)
                    .Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt, p.AuthorId))
                    .ToList();

            return !posts.Any() ? Results.Ok("No posts exist under this Author.") : Results.Ok(posts);
        }).WithDisplayName("Get Posts by Author").WithTags("Blog Post");

        // Create a new post*
        app.MapPost("/post", (Database database, PostRequest postRequest) =>
        {
            int currId = database.Posts.Count == 0 ? 0 : database.Posts.Max(post => post.Id);
            Post post = new Post { Id = currId + 1, Title = postRequest.Title, Content = postRequest.Content, AuthorId = postRequest.AuthorId, CreatedAt = DateTime.Now };
            database.Posts.Add(post);
            return Results.Created($"/post/{post.Id}", new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt, post.AuthorId));
        }).WithDisplayName("Create Post").WithTags("Blog Post");

        // Update a post*
        app.MapPut("/post/{id}", (Database database, PostRequest postRequest, int id) =>
        {
            var existingPost = database.Posts.FirstOrDefault(p => p.Id == id);
            if (existingPost is not null)
            {
                existingPost.Title = postRequest.Title;
                existingPost.Content = postRequest.Content;
                return Results.Ok(new PostResponse(existingPost.Id, existingPost.Title, existingPost.Content, existingPost.CreatedAt, existingPost.AuthorId));
            }
            else
            {
                return Results.NotFound("The post to be edited does not exist.");
            }
        }).WithDisplayName("Update Post").WithTags("Blog Post");

        // Delete a post*
        app.MapDelete("/post/{id}", (Database database, int id) =>
        {
            var post = database.Posts.FirstOrDefault(p => p.Id == id);
            if (post is not null)
            {
                database.Posts.Remove(post);
                return Results.Ok();
            }
            else
            {
                return Results.NotFound("Post not found.");
            }
        }).WithDisplayName("Delete Post").WithTags("Blog Post");
    }
}