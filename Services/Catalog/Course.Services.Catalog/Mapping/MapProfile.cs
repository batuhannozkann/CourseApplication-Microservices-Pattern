using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;

namespace Course.Services.Catalog.Mapping;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Models.Course, CourseDto>().ReverseMap();
        CreateMap<Models.Course, CourseCreateDto>().ReverseMap();
        CreateMap<Models.Course, CourseUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();
        
    }
}