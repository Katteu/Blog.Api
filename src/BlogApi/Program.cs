using UserModel;
using BlogModel;
using BlogEndpoints;
using UserEndpoints;

// Create a new WebApplicationBuilder instance
var builder = WebApplication.CreateBuilder(args);

// Add services for API explorer and Swagger generation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the WebApplication instance
var app = builder.Build();

// Check if the environment is development
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI for development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Create instances of UserEndpoint and BlogEndpoint
var userEndpoint = new UserEndpoint();
var blogEndpoint = new BlogEndpoint();

// Define User Endpoints
// Get all users
app.MapGet("/user", () => userEndpoint.GetAll());
// Get a user by Id
app.MapGet("/user/{id}", (int id) => userEndpoint.Get(id));
// Get a user by username
app.MapGet("/user/username/{username}", (string username) => userEndpoint.GetUserByUsername(username));
// Create a new user
app.MapPost("/user", (User user) => userEndpoint.Create(user));
// Update an existing user
app.MapPut("/user/{id}", (User user, int id) => userEndpoint.UpdateUser(user, id));
// Delete a user by Id
app.MapDelete("/user/{id}", (int id) => userEndpoint.Delete(id));

// Define Blog Endpoints
// Get all blog posts
app.MapGet("/blog", () => blogEndpoint.GetAll());
// Get a blog post by Id
app.MapGet("/blog/{id}", (int id) => blogEndpoint.Get(id));
// Get blog posts by author Id
app.MapGet("/blog/author/{Authorid}", (int Authorid) => blogEndpoint.GetPostByAuthor(Authorid));
// Create a new blog post
app.MapPost("/blog", (Blog post) => blogEndpoint.Create(post));
// Update an existing blog post
app.MapPut("/blog/{id}", (Blog post, int id) => blogEndpoint.UpdateBlog(post, id));
// Delete a blog post by Id
app.MapDelete("/blog/{id}", (int id) => blogEndpoint.Delete(id));

// Run the WebApplication
app.Run();