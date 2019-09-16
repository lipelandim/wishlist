using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Infrastructure.Repository
{
    public class BaseRepository
    {
        public readonly MongoDbContext dbContext;

        public BaseRepository(MongoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}