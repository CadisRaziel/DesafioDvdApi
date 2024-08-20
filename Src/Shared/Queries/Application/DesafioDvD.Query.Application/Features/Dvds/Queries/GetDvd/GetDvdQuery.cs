using MediatR;


namespace DesafioDvD.Query.Application.Features.Dvds.Queries.GetDvd
{
    public record GetDvdQuery(string title) : IRequest<GetDvdResponse>;
    //Nao iremos passar o Id pq o usuario nao sabe qual e o Id entao ele vai procurar pelo Titulo
}
