using MongoDB.Bson;

namespace LuizaLabs.Wishlist.Apis.Models.DTO
{
    public class ClientesDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string[] Wishlist { get; set; }
    }
}
