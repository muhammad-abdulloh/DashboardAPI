using System;
using System.Threading.Tasks;
using DashboardAPI.FunctionalTests.GenericTests;
using DashboardAPI.FunctionalTests.Helpers;
using DashboardAPI.Models.DTOs.Category;
using Xunit;

namespace DashboardAPI.FunctionalTests.Controllers
{
    [Collection("WebApplicationFactory")]
    public sealed class CategoriesController : AGenericTests<GetCategoryDto, AddCategoryDto, UpdateCategoryDto>
    {
        protected override IEntityHelper<GetCategoryDto, AddCategoryDto, UpdateCategoryDto> Helper { get; set; }
        public override async Task<GetCategoryDto> AddRandomEntity()
        {
            var category = new AddCategoryDto()
            {
                Name = Guid.NewGuid().ToString("N")
            };
            return await Helper.AddEntity(category);
        }

        public CategoriesController(TestWebApplicationFactory factory) : base(factory)
        {
            Helper = new CategoryHelper(Client);
        }
    }
}
