using MediatR;


namespace DesafioDvD.Query.Application.Features.Dvds.Commands.RentDvd
{
    public record RentDvdCommand(string Id, DateTime UpdatedAt) : IRequest<bool>;
}
