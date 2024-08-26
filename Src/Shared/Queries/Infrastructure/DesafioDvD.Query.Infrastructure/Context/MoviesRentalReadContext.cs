using DesafioDvD.Query.Domain.Models;
using DesafioDvD.Query.Infrastructure.Settings;
using MongoDB.Driver;

namespace DesafioDvD.Query.Infrastructure.Context
{
    public class MoviesRentalReadContext : IMoviesRentalReadContext
    {
        public MoviesRentalReadContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Dvds = database.GetCollection<Dvd>(settings.DvdsCollection);
            Directors = database.GetCollection<Director>(settings.DirectorsCollection);
        }
        public IMongoCollection<Dvd> Dvds { get; }

        public IMongoCollection<Director> Directors { get; }
    }
}
