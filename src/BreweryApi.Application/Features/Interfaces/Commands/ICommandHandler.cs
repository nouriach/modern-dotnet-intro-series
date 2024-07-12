using MediatR;

namespace BreweryApi.Application.Features.Interfaces.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
    where TCommand : IRequest<TResponse> {}