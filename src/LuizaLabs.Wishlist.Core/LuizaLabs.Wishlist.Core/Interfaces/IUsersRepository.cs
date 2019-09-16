using LuizaLabs.Wishlist.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Core.Interfaces
{
    public interface IUsersRepository
    {
        Users Find(Users users);
        void Add(Users users);
    }
}
