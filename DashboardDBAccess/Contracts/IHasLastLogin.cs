using System;

namespace DashboardDBAccess.Contracts
{
    public interface IHasLastLogin
    {
        public DateTimeOffset LastLogin { get; set; }
    }
}
