
using LuizaLabs.Wishlist.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LuizaLabs.Wishlist.Infrastructure.Repository
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Users> Users
        {
            get
            {
                return _database.GetCollection<Users>("Users");
            }
        }
        public IMongoCollection<Clientes> Clientes
        {
            get
            {
                return _database.GetCollection<Clientes>("Clientes");
            }
        }
    }
}
