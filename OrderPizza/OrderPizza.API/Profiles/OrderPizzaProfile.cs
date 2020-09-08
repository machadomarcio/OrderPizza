using AutoMapper;
using OrderPizza.Domain.Models;

namespace OrderPizza.API.Profiles
{
    public class OrderPizzaProfile : Profile
    {
        public OrderPizzaProfile()
        {
            CreateMap<Order, Order>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate));


            CreateMap<Pizza, Pizza>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Orderid, opt => opt.Ignore());


            CreateMap<PizzaFlavor, PizzaFlavor>()
                .ForMember(dest => dest.Flavor, opt => opt.Ignore())
                .ForMember(dest => dest.Pizza, opt => opt.Ignore())
                .ForMember(dest => dest.PizzaId, opt => opt.Ignore());

        }
    }
}
