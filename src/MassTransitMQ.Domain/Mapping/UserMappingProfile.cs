using AutoMapper;
using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.User;
using MassTransitMQ.Domain.Entities;

namespace MassTransitMQ.Domain.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserInput, User>().ReverseMap();
        CreateMap<UserOutput, User>().ReverseMap();
        CreateMap<UserInput, UserOutput>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserDto, UserInput>().ReverseMap();
        CreateMap<UserDto, UserOutput>().ReverseMap();
    }
}