

using DesafioDvD.Query.Domain.Models;
using MongoDB.Driver;

namespace DesafioDvD.Query.Infrastructure.Context
{
    public interface IMoviesRentalReadContext
    {
        IMongoCollection<Dvd> Dvds { get; }
        IMongoCollection<Director> Directors { get; }
    }
}
