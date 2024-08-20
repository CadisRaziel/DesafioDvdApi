using MediatR;

namespace DesafioDvD.Query.Application.Features.Dvds.Commands.CreateDvd
{
    public record CreateDvdCommand(
      string Id,
      string Title,
      string Genre,
      DateTime Published,
      bool Available,
      int Copies,
      string DirectorId,
      DateTime CreatedAt,
      DateTime UpdatedAt) : IRequest<bool>;
}
