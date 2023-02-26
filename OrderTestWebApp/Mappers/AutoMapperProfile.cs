using AutoMapper;

using OrderTestWebApp.DTOs;
using OrderTestWebApp.Models;

namespace OrderTestWebApp.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            _ = CreateMap<Order, OrderDTO>();
            _ = CreateMap<OrderDTO, Order>();
        }
    }
}
