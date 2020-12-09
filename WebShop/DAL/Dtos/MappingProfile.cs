using AutoMapper;
using DAL.Dtos.CategoryDTOS;
using DAL.Dtos.SubCategoryDTOS;
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
        }
    }
}
