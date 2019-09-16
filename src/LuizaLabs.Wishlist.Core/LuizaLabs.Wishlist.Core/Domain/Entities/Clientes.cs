
using MongoDB.Bson;

namespace LuizaLabs.Wishlist.Core.Domain.Entities
{
    public class Clientes
    {
        public object _id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string[] Wishlist { get; set; }
    }
}
