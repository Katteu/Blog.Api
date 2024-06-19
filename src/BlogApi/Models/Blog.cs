
namespace BlogModel;
public class Blog
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }

}