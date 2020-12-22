using AutoMapper;
using DAL.Dtos.CategoryDTOS;
using DAL.Dtos.CountryDTOS;
using DAL.Dtos.DiscountDTOS;
using DAL.Dtos.ItemBrandDTOS;
using DAL.Dtos.ItemDiscountDTOS;
using DAL.Dtos.ItemDTOS;
using DAL.Dtos.OrderHeaderDTOS;
using DAL.Dtos.PayMethodDTOS;
using DAL.Dtos.ShipAddressDTOS;
using DAL.Dtos.ShipCostDTOS;
using DAL.Dtos.SubCategoryDTOS;
using DAL.Dtos.TownDTOS;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();
            CreateMap<ItemBrand, ItemBrandDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<ItemDiscount, ItemDiscountDTO>().ReverseMap();
            CreateMap<Models.PayMethod, PayMethodDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<ShipCost, ShipCostDTO>().ReverseMap();
            CreateMap<Town, TownDTO>().ReverseMap();
            CreateMap<ShipAddress, ShipAddressDTO>().ReverseMap();

            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeader, AddOrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeader, EditOrderHeaderDTO>().ReverseMap();
        }
    }
}
