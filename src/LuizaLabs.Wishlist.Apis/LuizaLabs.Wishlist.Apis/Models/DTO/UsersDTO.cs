using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Apis.Models.DTO
{
    public class UsersDTO
    {
        public object _id { get; }
        public string userId { get; set; }
        public string accessKey { get; set; }
    }
}
