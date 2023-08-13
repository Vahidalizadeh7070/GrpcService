using GrpcService.Models;

namespace GrpcService.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddCategory(Category category)
        {
            if(category is not null)
            {
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
            }
            return category;
        }

        public IEnumerable<Category> AllCategories()
        {
            return _dbContext.Categories.OrderByDescending(c=>c.CreatedAt).ToList();
        }

        public bool DeleteCategoryById(string Id)
        {
            var categoryObject = _dbContext.Categories.FirstOrDefault(c=>c.Id == Id);
            if(categoryObject is not null)
            {
                _dbContext.Categories.Remove(categoryObject);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Category> EditCategory(Category category)
        {
            var categoryObject = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if(categoryObject is not null)
            {
                categoryObject.Name = category.Name;
                categoryObject.Enable = category.Enable;
                await _dbContext.SaveChangesAsync();
            }
            return category;
        }

        public Category GetCategoryById(string Id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == Id);
        }
    }
}
