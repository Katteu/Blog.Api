namespace Blog.Api.Endpoints;
using Microsoft.AspNetCore.Builder;

public interface IEndpointModule
{
    void RegisterEndpoints(IEndpointRouteBuilder app);
}