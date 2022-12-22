﻿using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<UserModel, UserApiModel>().ReverseMap();
            CreateMap<UsersModel, UsersApiModel>().ReverseMap();
        }
    }
}