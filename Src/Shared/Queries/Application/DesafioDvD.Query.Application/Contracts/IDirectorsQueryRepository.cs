using DesafioDvD.Query.Domain.Models;

namespace DesafioDvD.Query.Application.Contracts
{
    public interface IDirectorsQueryRepository : IQueryRepository<Director>
    {
        Task<Director> GetByName(string name);
    }
}

//Porque por name e nao por Id?
//Estamos estipulando que seja um USUARIO a procurar pelo diretor
//Ou seja ele nao vai fica gravando o ID de cada diretor, ele vai por o NOME do diretor e vai procurar