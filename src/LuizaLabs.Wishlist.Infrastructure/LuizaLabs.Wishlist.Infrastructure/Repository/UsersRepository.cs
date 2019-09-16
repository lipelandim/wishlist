using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        MongoDbContext _dbContext;
        public UsersRepository(IOptions<Settings> settings)
        {
            _dbContext = new MongoDbContext(settings);
        }

        public void Add(Users users)
        {
            _dbContext.Users.InsertOne(users);
        }

        public Users Find(Users users)
        {
            return _dbContext.Users.Find(x => x.userId == users.userId).FirstOrDefault();
        }
    }
}
