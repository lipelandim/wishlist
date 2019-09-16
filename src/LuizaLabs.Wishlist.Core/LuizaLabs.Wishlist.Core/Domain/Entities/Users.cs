using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Core.Domain.Entities
{
    public class Users
    {
        public object _id { get; }
        public string userId { get; set; }
        public string accessKey { get; set; }
    }
}
