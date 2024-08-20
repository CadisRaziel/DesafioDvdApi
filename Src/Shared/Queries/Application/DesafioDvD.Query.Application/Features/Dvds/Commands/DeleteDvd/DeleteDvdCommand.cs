using MediatR;


namespace DesafioDvD.Query.Application.Features.Dvds.Commands.DeleteDvd
{
    public record DeleteDvdCommand(string Id, DateTime DeletedAt) : IRequest<bool>;
}
