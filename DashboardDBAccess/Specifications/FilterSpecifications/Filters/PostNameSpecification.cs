﻿using System;
using System.Linq.Expressions;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Specifications.FilterSpecifications.Filters
{
    public class PostNameSpecification<TEntity> : FilterSpecification<TEntity> where TEntity : class, IPoco, IHasPost 
    {
        private readonly string _name;

        public PostNameSpecification(string name)
        {
            _name = name;
        }

        protected override Expression<Func<TEntity, bool>> SpecificationExpression => p => p.Post != null && p.Post.Name == _name;
    }
}
