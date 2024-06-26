using Blog.Api.Data;
using Blog.Api.Services;
using Blog.Api.Endpoints;
using Microsoft.OpenApi.Models;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
// Create a new WebApplicationBuilder instance
var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conn));

// Add services for API explorer and Swagger generation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
    // Define Swagger documents
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.API", Version = "v1" });
});

builder.Services.AddScoped<PostServices>();
builder.Services.AddScoped<UserServices>();

// Build the WebApplication instance
var app = builder.Build();

// Check if the environment is development
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI for development environment
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.RegisterEndpointModules();


app.Run();