﻿using AutoMapper;
using Yarish.University.Filmark.Models.Database;
using Yarish.University.Filmark.Models.Frontend;
using Yarish.University.Filmark.Core.Interfaces;

namespace Yarish.University.Filmark.Core.Mapper
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public MapperProvider()
        {
            _mapper = Initialize();
        }

        public IMapper GetMapper() => _mapper;

        private IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, User>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                    .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}