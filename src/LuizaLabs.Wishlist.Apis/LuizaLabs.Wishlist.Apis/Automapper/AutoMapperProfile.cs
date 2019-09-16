using AutoMapper;
using LuizaLabs.Wishlist.Apis.Automapper.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Apis.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UsersMapper.Map(this);
        }
    }
}
