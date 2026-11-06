using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Order;



namespace Ecom.Application.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<Orders , OrderResponse>()
                .ForMember(d=>d.deliveryMethod, o=>o.MapFrom(src =>src.deliveryMethod.Name))
                .ReverseMap();
            CreateMap<OrderItem , OrderItemDTO>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDTO>().ReverseMap();
            CreateMap<Address, ShippingAddressDTO>().ReverseMap();
        }
    }
}
