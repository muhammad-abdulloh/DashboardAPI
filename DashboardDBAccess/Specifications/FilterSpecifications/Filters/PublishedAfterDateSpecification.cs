using System;
using System.Linq.Expressions;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Specifications.FilterSpecifications.Filters
{
    public class PublishedAfterDateSpecification<TEntity> : FilterSpecification<TEntity>
        where TEntity : class, IPoco, IHasCreationDate
    {
        private readonly DateTimeOffset _date;

        public PublishedAfterDateSpecification(DateTimeOffset date)
        {
            _date = date;
        }

        protected override Expression<Func<TEntity, bool>> SpecificationExpression => p => p.PublishedAt > _date;
    }
}
