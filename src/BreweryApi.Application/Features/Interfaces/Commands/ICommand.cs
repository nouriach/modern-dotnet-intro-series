using MediatR;

namespace BreweryApi.Application.Features.Interfaces.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse> { }