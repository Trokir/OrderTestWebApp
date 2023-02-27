using AutoMapper;

using OrderTestWebApp.DTOs;
using OrderTestWebApp.Models;

namespace OrderTestWebApp.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            _ = CreateMap<Order, OrderDTO>()
                .ForMember(x => x.OrderType, opt => opt.MapFrom(t => t.OrderType.ToString()));
            _ = CreateMap<OrderDTO, Order>();
            _ = CreateMap<OrderInsertDTO, Order>();
            _ = CreateMap<Order, OrderInsertDTO>();
            _ = CreateMap<OrderUpdateDTO, Order>();
            _ = CreateMap<Order, OrderUpdateDTO>();

        }
    }
}
