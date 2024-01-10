using System.Collections.Generic;
using DashboardDBAccess.Data.JoiningEntity;

namespace DashboardDBAccess.Contracts
{
    public interface IHasPostTag
    {
        public ICollection<PostTag> PostTags { get; set; }
    }
}
