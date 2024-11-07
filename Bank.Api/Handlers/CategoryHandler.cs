using Bank.Api.Data;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
      try{
         var category = new Category
    {
      UserId = request.UserId,
      Title = request.Title,
      Description = request.Description
    };

    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();
    
    return new Response<Category?>(category, 201, "Categoria criada com sucesso");
      }
      catch
      {
           return new Response<Category?>(null, 500, "Não foi possivel criar a categoria");

      }
    }
    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
         try
       { var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id  && x.UserId == request.UserId);

        if (category is null)
        return new Response<Category?>(null, 404, "Categoria não encontrada");

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return new Response<Category?>(category, message: "Categoria deletada com sucesso");
    }
    catch
    {
        return new Response<Category?>(null, 500, "Não foi possivel deletar a categoria");
    }
    }
    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
     try
     {
       var query = context
      .Categories
      .AsNoTracking()
      .Where(x => x.UserId == request.UserId)
      .OrderBy(x => x.Title);

      var categories = await query
      .Skip((request.PageNumber - 1 ) * request.PageSize)
      .Take(request.PageSize)
      .ToListAsync();


      var count = await query.CountAsync();

      return new PagedResponse<List<Category>>(
          categories, 
          count, 
          request.PageNumber, 
          request.PageSize);
     }
     catch
     {
      return new PagedResponse<List<Category>>(null, 500, "Não foi possivel buscar as categorias");
     }
    }


    public async Task<Response<Category?>> GetByIdAsync(GetCategoriesByIdRequest request)
    {
      try{
        var category = await context
        .Categories
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.Id  && x.UserId == request.UserId);

      return category is null
      ? new Response<Category?>(null, 404, "Categoria não encontrada")
      : new Response<Category?>(category);
      }
      catch
      {
        return new Response<Category?>(null, 500, "Não foi possivel buscar a categoria");
      }
   }
    

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
       try
       { var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id  && x.UserId == request.UserId);

        if (category is null)
        return new Response<Category?>(null, 404, "Categoria não encontrada");
        
        category.Title = request.Title;
        category.Description = request.Description;
        context.Categories.Update(category);
        
        await context.SaveChangesAsync();

        return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
    }
        catch
        {
          return new Response<Category?>(null, 500, "[FP079] Não foi possivel atualizar a categoria");
        }
}
}
