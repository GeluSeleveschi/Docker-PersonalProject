using AutoMapper;
using PersonalProject.BLL.Entities;
using PersonalProject.BLL.Models;

namespace PersonalProject.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
