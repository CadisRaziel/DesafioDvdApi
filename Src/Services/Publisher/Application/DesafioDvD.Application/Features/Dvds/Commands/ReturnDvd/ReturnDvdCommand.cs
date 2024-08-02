using MediatR;

namespace DesafioDvD.Application.Features.Dvds.Commands.ReturnDvd
{
    public record ReturnDvdCommand(Guid Id) : IRequest<ReturnDvdResponse>;    
}
