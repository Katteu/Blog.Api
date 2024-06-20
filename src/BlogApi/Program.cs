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

var userEndpoint = new UserEndpoint();
var blogEndpoint = new BlogEndpoint();

//User Endpoints
app.MapGet("/user", () => userEndpoint.GetAll());
app.MapGet("/user/{id}", (int id) => userEndpoint.Get(id));
app.MapGet("/user/username/{id}", (string username) => userEndpoint.GetUserByUsername(username));
app.MapPost("/user", (User user) => userEndpoint.Create(user));
app.MapPut("/user/{id}", (User user, int id) => userEndpoint.UpdateUser(user, id));
app.MapDelete("/user/{id}", (int id) => userEndpoint.Delete(id));

//Blog Endpoints
app.MapGet("/blog", () => blogEndpoint.GetAll());
app.MapGet("/blog/{id}", (int id) => blogEndpoint.Get(id));
app.MapGet("/blog/author/{Authorid}", (int Authorid) => blogEndpoint.GetPostByAuthor(Authorid));
app.MapPost("/blog", (Blog post) => blogEndpoint.Create(post));
app.MapPut("/blog/{id}", (Blog post, int id) => blogEndpoint.UpdateBlog(post, id));
app.MapDelete("/blog/{id}", (int id) => blogEndpoint.Delete(id));

app.Run();


