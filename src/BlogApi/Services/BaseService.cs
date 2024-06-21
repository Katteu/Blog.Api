using Blog.Api.Data;
using Blog.Api.Models;

namespace Blog.Api.Services;

public interface IEntity
{
    int Id { get; set; }
}

public abstract class BaseService<TModel, TRequest, TResponse> where TModel : IEntity
{
    protected readonly Database<TModel> _database;

    public BaseService(Database<TModel> database)
    {
        _database = database;
    }

    public IResult GetAll()
    {
        return Results.Ok(_database.GetAll().Select(MapToResponse).ToList());
    }

    public IResult GetById(int id)
    {
        var model = _database.GetById(id);
        return model is not null ? Results.Ok(MapToResponse(model)) : Results.NotFound("Entity or record not found.");
    }

    public virtual IResult Create(TRequest request)
    {
        var model = MapToModel(request);
        _database.Add(model);
        return Results.Ok(MapToResponse(model));
    }

    public IResult Delete(int id)
    {
        var model = _database.GetById(id);
        if (model is null)
        {
            return Results.NotFound("Entity or record cannot be found.");
        }
        _database.Remove(model);
        return Results.Ok(MapToResponse(model));
    }

    protected abstract TResponse MapToResponse(TModel model);
    protected abstract TModel MapToModel(TRequest request, TModel? existingModel = default);
}
