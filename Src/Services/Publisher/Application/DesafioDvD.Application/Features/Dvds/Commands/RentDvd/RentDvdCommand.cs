using MediatR;

namespace DesafioDvD.Application.Features.Dvds.Commands.RentDvd
{
    //Atualizacao do estado do dvd (Alugar um DVD)
    public record RentDvdCommand(Guid Id) : IRequest<RentDvdResponse>;   
}
