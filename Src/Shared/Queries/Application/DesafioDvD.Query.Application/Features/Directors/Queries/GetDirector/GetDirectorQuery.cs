using MediatR;

namespace DesafioDvD.Query.Application.Features.Directors.Queries.GetDirector
{
    public record GetDirectorQuery(string FullName) : IRequest<GetDirectorResponse>;
}

//Querys e commands aos olhos do mediator eles sao requisicoes
//Por isso nossa Query tipa um IRequest