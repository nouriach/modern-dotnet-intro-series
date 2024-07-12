using MediatR;

namespace BreweryApi.Application.Features.Interfaces.Queries;

public interface IQuery<out T> : IRequest<T> { }