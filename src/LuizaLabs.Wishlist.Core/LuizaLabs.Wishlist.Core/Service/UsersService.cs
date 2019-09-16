using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Interfaces;
using LuizaLabs.Wishlist.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Core.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;    
        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(Users users)
        {
            _userRepository.Add(users);
        }

        public Users Find(Users users)  
        {
            return _userRepository.Find(users);
        }
    }
}
