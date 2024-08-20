
using DesafioDvD.Query.Domain.Models;

namespace DesafioDvD.Query.Application.Contracts
{
    public interface IDvdsQueryRepository : IQueryRepository<Dvd>
    {
        Task<Dvd> GetByTitle(string title);
    }
}
