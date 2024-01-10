using System;

namespace DashboardDBAccess.Contracts
{
    public interface IHasModificationDate
    {
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}
