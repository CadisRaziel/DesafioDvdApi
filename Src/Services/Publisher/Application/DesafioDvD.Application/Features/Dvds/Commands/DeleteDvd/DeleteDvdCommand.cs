
using MediatR;

namespace DesafioDvD.Application.Features.Dvds.Commands.DeleteDvd
{
    public record DeleteDvdCommand(Guid Id) : IRequest<DeleteDvdResponse>;

}
