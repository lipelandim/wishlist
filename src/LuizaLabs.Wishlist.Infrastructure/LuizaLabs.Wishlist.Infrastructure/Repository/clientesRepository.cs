using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoRepository;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Infrastructure.Repository
{
    public class ClientesRepository : IClientesRepository
    {

        MongoDbContext _dbContext;
        public ClientesRepository(IOptions<Settings> settings)
        {
            _dbContext = new MongoDbContext(settings);
        }

        public Clientes VerificarUsuario(string email)
        {
            return _dbContext.Clientes.Find(us => us.Email == email).FirstOrDefault();
        }

        public async Task<bool> AdicionarProdutoAsync(Clientes cli)
        {
            var filter = Builders<Clientes>.Filter.Eq("_id", cli._id);
            var update = Builders<Clientes>.Update.Set("Wishlist", cli.Wishlist);

            UpdateResult actionResult = await _dbContext.Clientes.UpdateOneAsync(filter, update);

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        public void CadastrarCliente(Clientes cli)
        {
            _dbContext.Clientes.InsertOne(cli);
        }

        public void RemoverCliente(Clientes cli)
        {
            var filter = Builders<Clientes>.Filter.Eq("_id", cli._id);

            _dbContext.Clientes.DeleteOne(filter);
        }

        public void RemoverProduto(Clientes cli)
        {
            var filter = Builders<Clientes>.Filter.Eq("_id", cli._id);
            var update = Builders<Clientes>.Update.Set("Wishlist", cli.Wishlist);



            UpdateResult actionResult = _dbContext.Clientes.UpdateOne(filter, update);
        }
    }
}
