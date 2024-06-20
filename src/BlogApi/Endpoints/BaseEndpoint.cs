namespace BaseEndpoints;

// Define an interface for entities with an Id property
public interface IEntity
{
    int Id { get; set; }
}

// Abstract base class for endpoints that work with entities
public abstract class BaseEndpoint<T> where T : class, IEntity
{
    // List of entities stored in memory
    protected readonly List<T> _entities;

    public BaseEndpoint()
    {
        _entities = new List<T>();
    }

    // Get all entities
    public virtual List<T> GetAll()
    {
        return _entities;
    }

    // Get an entity by Id
    public virtual T? Get(int id)
    {
        // Find the entity with the matching Id
        return _entities.Find(entity => entity.Id == id);
    }

    // Create a new entity
    public virtual IResult Create(T entity)
    {
        // Generate a new Id for the entity
        int currId = !_entities.Any() ? 0 : _entities.Max(e => e.Id);
        entity.Id = currId + 1;
        
        // Add the entity to the list
        _entities.Add(entity);
        return Results.Ok(entity);
    }

    // Delete an entity by Id
    public virtual IResult Delete(int id)
    {
        // Find the entity with the matching Id
        T? entity = Get(id);

        if (entity is null)
        {
            // Return a 404 error if the entity is not found
            return Results.NotFound("Entity or record cannot be found.");
        }

        // Remove the entity from the list
        _entities.Remove(entity);
        return Results.Ok(entity);
    }
}