
using MediatR;

namespace DesafioDvD.Query.Application.Features.Dvds.Commands.UpdateDvd
{
    public record UpdateDvdCommand(
        string Id,
        string Title,
        string Genre,
        DateTime Published,
        int Copies,
        string DirectorId,
        DateTime UpdatedAt
        ) : IRequest<bool>;
}
