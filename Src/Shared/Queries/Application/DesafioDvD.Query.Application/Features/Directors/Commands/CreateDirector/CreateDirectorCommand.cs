using MediatR;

namespace DesafioDvD.Query.Application.Features.Directors.Commands.CreateDirector
{
    public record CreateDirectorCommand(string Id, string FullName, DateTime CreatedAt, DateTime UpdatedAt) : IRequest<bool>;
    //Vai ser usado para criar objeto no mongoDb por isso o Id como string
}

//IRequest<bool> -> Como vamos criar um objeto para o banco de leitura (mongo) a gente nao precisa que ele retorna uma resposta(Response) com os dados, so precisamos saber se o objeto foi criado ou nao
