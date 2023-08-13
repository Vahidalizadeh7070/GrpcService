using GrpcService.Models;
using System.Collections.Generic;

namespace GrpcService.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(Category category);
        Task<Category> EditCategory(Category category);
        IEnumerable<Category> AllCategories();
        Category GetCategoryById(string Id);
        bool DeleteCategoryById(string Id);
    }
}
