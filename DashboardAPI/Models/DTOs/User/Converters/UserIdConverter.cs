using AutoMapper;
using DashboardDBAccess.Repositories.User;

namespace DashboardAPI.Models.DTOs.User.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="User"/> to its resource Id.
    /// </summary>
    public class UserIdConverter : ITypeConverter<int, DashboardDBAccess.Data.User>
    {
        private readonly IUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdConverter"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public UserIdConverter(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public DashboardDBAccess.Data.User Convert(int source, DashboardDBAccess.Data.User destination,
            ResolutionContext context)
        {
            return _repository.Get(source);
        }
    }
}
