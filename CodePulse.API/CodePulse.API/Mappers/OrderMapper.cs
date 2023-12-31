﻿using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
        CreateMap<Order, OrderDto>();
    }
}