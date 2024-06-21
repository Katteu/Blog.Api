using System.Reflection;
using Blog.Api.Endpoints;

public static class EndpointExtensions
{
    public static void RegisterEndpointModules(this IEndpointRouteBuilder endpoints)
    {
        var modules = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEndpointModule).IsAssignableFrom(t) && t.IsClass)
            .Select(t => (IEndpointModule?)Activator.CreateInstance(t));

        foreach (var module in modules)
        {
            module?.RegisterEndpoints(endpoints);
        }
    }
}