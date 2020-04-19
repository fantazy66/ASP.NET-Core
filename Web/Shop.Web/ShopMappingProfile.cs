using AutoMapper;
using Shop.Data.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            this.CreateMap<Order, OrderViewModel>()
            .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
            .ReverseMap();   // reverseMap, za da moje da se mapva i ot viewModel kum class.
        }
    }
}
