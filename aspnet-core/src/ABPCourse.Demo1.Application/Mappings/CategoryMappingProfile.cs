using ABPCourse.Demo1.Categories;
using AutoMapper;

namespace ABPCourse.Demo1.Mappings
{
    public class CategoryMappingProfile : Profile
    {


        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            //.ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr));
            CreateMap<CreateUpdateCategoryDto, Category>();

        }
    }
}
