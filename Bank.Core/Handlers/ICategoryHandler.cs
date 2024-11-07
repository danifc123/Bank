using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;

namespace Bank.Core.Handlers;

public interface ICategoryHandler
{
  Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
  Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
  Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
  Task<Response<Category?>> GetByIdAsync(GetCategoriesByIdRequest request);
  Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}