using AutoMapper;
using DashboardDBAccess.Repositories.Category;

namespace DashboardAPI.Models.DTOs.Category.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="Category"/> to its resource Id.
    /// </summary>
    public class CategoryIdConverter : ITypeConverter<int, DashboardDBAccess.Data.Category>
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryIdConverter"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public CategoryIdConverter(ICategoryRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public DashboardDBAccess.Data.Category Convert(int source, DashboardDBAccess.Data.Category destination, ResolutionContext context)
        {
            return _repository.Get(source);
        }
    }
}
