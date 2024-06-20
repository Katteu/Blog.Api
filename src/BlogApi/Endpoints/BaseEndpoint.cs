namespace BaseEndpoints;

public interface IEntity
{
    int Id { get; set; }
}

public abstract class BaseEndpoint<T> where T : class, IEntity
{

    protected readonly List<T> _entities;

    public BaseEndpoint()
    {
        _entities = new List<T>();
    }

    public virtual List<T> GetAll()
    {
        return _entities;
    }

    public virtual T? Get(int id)
    {
        return _entities.Find(entity => entity.Id == id);
    }

    public virtual IResult Create(T entity)
    {
        int currId = !_entities.Any() ? 0 : _entities.Max(e => e.Id);
        entity.Id = currId + 1;
        
        _entities.Add(entity);
        return Results.Ok(entity);
    }

    public virtual IResult Delete(int id)
    {
        T? entity = Get(id);

        if (entity is null)
        {
            return Results.NotFound("Entity or record cannot be found.");
        }

        _entities.Remove(entity);
        return Results.Ok(entity);
    }
}