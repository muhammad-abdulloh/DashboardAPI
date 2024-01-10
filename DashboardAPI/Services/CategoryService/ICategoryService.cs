using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.Models.DTOs.Category;
using DashboardDBAccess.Data;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetAllCategories();

        public Task<IEnumerable<GetCategoryDto>> GetCategories(FilterSpecification<Category> filterSpecification = null,
            PagingSpecification pagingSpecification = null,
            SortSpecification<Category> sortSpecification = null);

        public Task<int> CountCategoriesWhere(FilterSpecification<Category> filterSpecification = null);

        Task<GetCategoryDto> GetCategory(int id);

        Task<GetCategoryDto> AddCategory(AddCategoryDto category);

        Task UpdateCategory(UpdateCategoryDto category);

        Task DeleteCategory(int id);
    }
}
