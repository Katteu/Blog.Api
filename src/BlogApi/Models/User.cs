using BlogModel;
using BaseEndpoints;
namespace UserModel;

public class User : IEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

}