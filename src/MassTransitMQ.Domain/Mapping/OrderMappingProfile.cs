using AutoMapper;
using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.Order;
using MassTransitMQ.Domain.Entities;

namespace MassTransitMQ.Domain.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderInput, Order>().ForMember(dst => dst.Id, cfg => cfg.AllowNull()).ReverseMap();
        CreateMap<OrderOutput, Order>().ReverseMap();
        CreateMap<OrderInput, OrderOutput>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderDto, OrderInput>().ReverseMap();
        CreateMap<OrderDto, OrderOutput>().ReverseMap();
    }
}