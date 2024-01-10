using System;

namespace DashboardDBAccess.Contracts
{
    public interface IHasRegisteredAt
    {
        public DateTimeOffset RegisteredAt { get; set; }
    }
}
