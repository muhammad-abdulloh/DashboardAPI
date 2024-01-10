using System;
using System.Threading.Tasks;
using DashboardAPI.FunctionalTests.GenericTests;
using DashboardAPI.FunctionalTests.Helpers;
using DashboardAPI.Models.DTOs.Tag;
using Xunit;

namespace DashboardAPI.FunctionalTests.Controllers
{
    [Collection("WebApplicationFactory")]
    public sealed class TagsController : AGenericTests<GetTagDto, AddTagDto, UpdateTagDto>
    {
        protected override IEntityHelper<GetTagDto, AddTagDto, UpdateTagDto> Helper { get; set; }
        public override async Task<GetTagDto> AddRandomEntity()
        {
            var tag = new AddTagDto()
            {
                Name = Guid.NewGuid().ToString("N")
            };
            return await Helper.AddEntity(tag);
        }

        public TagsController(TestWebApplicationFactory factory) : base(factory)
        {
            Helper = new TagHelper(Client);
        }
    }
}
