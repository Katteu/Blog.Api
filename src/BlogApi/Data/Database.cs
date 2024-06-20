using Blog.Api.Models;

namespace Blog.Api.Data;

public class Database{
    public List<Post> Posts { get; set; } = [];
    public List<User> Users { get; set; } = [];
    
}
