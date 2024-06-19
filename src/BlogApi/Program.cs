using UserModel;
using BlogModel;
using BlogEndpoint;
using UserEndpoint;

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
app.MapGet("/user", () => UserEP.GetAllUsers());
app.MapGet("/user/{id}", (int id) => UserEP.GetUser(id));
app.MapPost("/user", (User user) => UserEP.CreateUser(user));
app.MapPut("/user/{id}", (User user,int id) => UserEP.UpdateUser(user,id));
app.MapDelete("/user/{id}", (int id) => UserEP.DeleteUser(id));



//Blog Endpoints
app.MapGet("/blog", () => BlogEP.GetAllPosts());
app.MapGet("/blog/{id}", (int id) => BlogEP.GetPost(id));
app.MapGet("/blog/author/{Authorid}", (int Authorid) => BlogEP.GetPostByAuthor(Authorid));
app.MapPost("/blog", (Blog post) => BlogEP.CreatePost(post));
app.MapPut("/blog/{id}", (Blog post,int id) => BlogEP.UpdatePost(post,id));
app.MapDelete("/blog/{id}", (int id) => BlogEP.DeletePost(id));

app.Run();