using System;

namespace DashboardDBAccess.Contracts
{
    public interface IHasCreationDate
    {
        public DateTimeOffset PublishedAt { get; set; }
    }
}
