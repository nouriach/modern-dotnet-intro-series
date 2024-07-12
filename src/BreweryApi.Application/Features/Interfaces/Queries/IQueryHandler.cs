using MediatR;

namespace BreweryApi.Application.Features.Interfaces.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IRequest<TResponse> { }