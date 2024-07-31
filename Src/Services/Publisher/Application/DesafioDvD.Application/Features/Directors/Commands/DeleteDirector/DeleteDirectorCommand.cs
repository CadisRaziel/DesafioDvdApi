using MediatR;

namespace DesafioDvD.Application.Features.Directors.Commands.DeleteDirector
{
    public record DeleteDirectorCommand(Guid id) : IRequest<bool>;   
    //Para deletar preciso apenas do Id
}

