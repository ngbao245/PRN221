using AutoMapper;
using CodeInBlue.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}
