using LuizaLabs.Wishlist.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace LuizaLabs.Wishlist.Core.Service
{
    public interface IUsersService
    {
        Users Find(Users users);
        void Add(Users users);
    }
}
