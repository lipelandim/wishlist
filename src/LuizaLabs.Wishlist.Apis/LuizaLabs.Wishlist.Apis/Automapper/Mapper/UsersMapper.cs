using AutoMapper;
using LuizaLabs.Wishlist.Apis.Models.DTO;
using LuizaLabs.Wishlist.Apis.Models.ViewModels;
using LuizaLabs.Wishlist.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Apis.Automapper.Mapper
{
    public static class UsersMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<UsersDTO, Users>();
            profile.CreateMap<UsersViewModel, Users>().ForMember(dest => dest._id, act => act.Ignore());

       }
    }
}
