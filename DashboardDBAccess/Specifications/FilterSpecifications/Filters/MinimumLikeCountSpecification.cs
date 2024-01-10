using System;
using System.Linq.Expressions;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Specifications.FilterSpecifications.Filters
{
    public class MinimumLikeCountSpecification<TEntity> : FilterSpecification<TEntity> where TEntity : class, IPoco, IHasLikes
    {
        private readonly int _number;

        public MinimumLikeCountSpecification(int number)
        {
            _number = number;
        }

        protected override Expression<Func<TEntity, bool>> SpecificationExpression => p => p.Likes.Count >= _number;
    }
}
