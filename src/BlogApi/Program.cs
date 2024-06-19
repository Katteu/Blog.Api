using UserModel;
using BlogModel;
using BlogEndpoints;
using UserEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//User Endpoints
app.MapGet("/user", () => UserEndpoint.GetAllUsers());
app.MapGet("/user/{id}", (int id) => UserEndpoint.GetUser(id));
app.MapPost("/user", (User user) => UserEndpoint.CreateUser(user));
app.MapPut("/user/{id}", (User user, int id) => UserEndpoint.UpdateUser(user, id));
app.MapDelete("/user/{id}", (int id) => UserEndpoint.DeleteUser(id));



//Blog Endpoints
app.MapGet("/blog", () => BlogEndpoint.GetAllPosts());
app.MapGet("/blog/{id}", (int id) => BlogEndpoint.GetPost(id));
app.MapGet("/blog/author/{Authorid}", (int Authorid) => BlogEndpoint.GetPostByAuthor(Authorid));
app.MapPost("/blog", (Blog post) => BlogEndpoint.CreatePost(post));
app.MapPut("/blog/{id}", (Blog post, int id) => BlogEndpoint.UpdatePost(post, id));
app.MapDelete("/blog/{id}", (int id) => BlogEndpoint.DeletePost(id));

app.Run();