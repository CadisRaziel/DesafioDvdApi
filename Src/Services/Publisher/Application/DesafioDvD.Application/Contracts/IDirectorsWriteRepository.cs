
using DesafioDvD.Domain.Entities;

namespace DesafioDvD.Application.Contracts
{
    //Repositorio de escrita
    public interface IDirectorsWriteRepository : IWriteRepository<Director>
    {
        Task<Director> GetDirectorWithMovies(Guid Id);
    }
}
