using AutoMapper;
using CategoryServiceGrpc;
using GrpcService.Models;

namespace GrpcService.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            // Create Request
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, CreateCategoryResponse>();

            // Edit Request
            CreateMap<EditCategoryRequest, Category>();
            CreateMap<Category, EditCategoryResponse>();

            // Delete
            CreateMap<Category, DeleteCategoryResponse>();

            // Get By Id
            CreateMap<Category, GetCategoryResponse>();

        }
    }
}
