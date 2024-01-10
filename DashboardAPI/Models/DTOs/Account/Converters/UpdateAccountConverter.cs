using AutoMapper;

namespace DashboardAPI.Models.DTOs.Account.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="UpdateAccountDto"/> to <see cref="User"/>.
    /// </summary>
    public class UpdateAccountConverter : ITypeConverter<UpdateAccountDto, DashboardDBAccess.Data.User>
    {
        /// <inheritdoc />
        public DashboardDBAccess.Data.User Convert(UpdateAccountDto source, DashboardDBAccess.Data.User destination,
            ResolutionContext context)
        {
            destination.ProfilePictureUrl = string.IsNullOrEmpty(source.ProfilePictureUrl) ? null : source.ProfilePictureUrl;
            destination.UserDescription = source.UserDescription;
            destination.Email = source.Email;
            destination.UserName = source.UserName;
            return destination;
        }
    }
}
