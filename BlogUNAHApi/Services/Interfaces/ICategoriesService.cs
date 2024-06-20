using BlogUNAHApi.Database.Entities;
using BlogUNAHApi.Dtos.Categories;

namespace BlogUNAHApi.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoryDto>> GetCategoriesListAsync();

        Task<CategoryDto> GetCategoryByIdAsync(Guid id);

        Task<bool> CreateAsync(CategoryCreateDto dto);
        Task<bool> EditAsync(CategoryEditDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);

    }
}
