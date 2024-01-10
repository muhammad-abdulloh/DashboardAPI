using AutoMapper;

namespace DashboardAPI.Models.DTOs.Category.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="UpdateCategoryDto"/> to <see cref="Category"/>.
    /// </summary>
    public class UpdateCategoryConverter : ITypeConverter<UpdateCategoryDto, DashboardDBAccess.Data.Category>
    {
        /// <inheritdoc />
        public DashboardDBAccess.Data.Category Convert(UpdateCategoryDto source, DashboardDBAccess.Data.Category destination,
            ResolutionContext context)
        {
            destination.Name = source.Name;
            return destination;
        }
    }
}
