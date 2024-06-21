using Blog.Api.Models;
using Blog.Api.Services;
namespace Blog.Api.Data;

public class Database<T> where T : IEntity

{
    public List<T> Data { get; set; } = new List<T>();

    public void Add(T item)

    {
        int currId = Data.Count == 0 ? 1 : Data.Max(m => m.Id) + 1;
        item.Id = currId;
        Data.Add(item);
    }


    public void Remove(T item)

    {

        Data.Remove(item);

    }


    public T? GetById(int? id)

    {
        return Data.FirstOrDefault(item => item.Id == id);
    }


    public List<T> GetAll()

    {

        return Data;

    }

}