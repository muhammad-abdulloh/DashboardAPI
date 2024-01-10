using System;
using System.Linq.Expressions;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Specifications.FilterSpecifications.Filters
{
    public class NameContainsSpecification<TEntity> : FilterSpecification<TEntity> where TEntity : class, IPoco, IHasName
    {
        private readonly string _name;

        public NameContainsSpecification(string name)
        {
            _name = name;
        }

        protected override Expression<Func<TEntity, bool>> SpecificationExpression => p => p.Name.Contains(_name);
    }
}
