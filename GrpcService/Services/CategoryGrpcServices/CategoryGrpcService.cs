using AutoMapper;
using CategoryServiceGrpc;
using Grpc.Core;
using GrpcService.Models;
using GrpcService.Services.CategoryService;

namespace GrpcService.Services.CategoryGrpcServices
{
    public class CategoryGrpcService : Categories.CategoriesBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryGrpcService> _logger;
        private readonly IMapper _mapper;

        public CategoryGrpcService(ICategoryService categoryService, ILogger<CategoryGrpcService> logger, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        // Create Category
        public override async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request, ServerCallContext context)
        {
            var mapRequestToCategory = _mapper.Map<Category>(request);  
            var res = await _categoryService.AddCategory(mapRequestToCategory);
            if(res is not null)
            {
                var mapRes = _mapper.Map<CreateCategoryResponse>(res);
                return await Task.FromResult(mapRes);
            }
            throw new RpcException(new Status(StatusCode.Cancelled,"Create Category Failed!"));
        }

        // Edit Category
        public override async Task<EditCategoryResponse> EditeCategory(EditCategoryRequest request, ServerCallContext context)
        {
            var mapRequestToCategory = _mapper.Map<Category>(request);
            var res = await _categoryService.EditCategory(mapRequestToCategory);
            if(res is not null)
            {
                var mapRes = _mapper.Map<EditCategoryResponse>(res);
                return await Task.FromResult(mapRes);
            }
            throw new RpcException(new Status(StatusCode.Cancelled, "Edit Category Failed!"));
        }

        // Delete Category
        public override async Task<DeleteCategoryResponse> DeleteCategory(DeleteCategoryRequest request, ServerCallContext context)
        {
            var category = _categoryService.GetCategoryById(request.Id);
            if(category is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "There is no category"));
            }
            var res = _categoryService.DeleteCategoryById(request.Id);
            if(res == true)
            {
                var mapCategory = _mapper.Map<DeleteCategoryResponse>(category);
                return await Task.FromResult(mapCategory);
            }
            throw new RpcException(new Status(StatusCode.Cancelled, "Deleting failed!"));
        }

        // Get Category By Id
        public override async Task<GetCategoryResponse> GetCategoryById(GetCategoryRequest request, ServerCallContext context)
        {
            var category = _categoryService.GetCategoryById(request.Id);
            if(category is not null)
            {
                var mapCategory = _mapper.Map<GetCategoryResponse>(category);
                return await Task.FromResult(mapCategory);
            }
            throw new RpcException(new Status(StatusCode.NotFound, "Category not found"));
        }

        // List Of All Categories
        public override async Task<GetAllCategoriesResponse> ListCategories(GetAllCategoriesRequest request, ServerCallContext context)
        {
            var response = new GetAllCategoriesResponse();

            var list = _categoryService.AllCategories();
            foreach (var item in list)
            {
                var mapModel = _mapper.Map<GetCategoryResponse>(item);
                response.Category.Add(mapModel);
            }

            return await Task.FromResult(response);
        }
    }
}
