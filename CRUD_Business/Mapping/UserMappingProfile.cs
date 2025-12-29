using AutoMapper;
using CRUD_Contracts.Users;
using CRUD_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Business.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // CreateUserDto → User dönüşümü
            CreateMap<CreateUserDto, User>();
        }
    }
}
