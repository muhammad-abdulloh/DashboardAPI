﻿using System;
using System.Linq.Expressions;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Specifications.FilterSpecifications.Filters
{
    public class UsernameSpecification<TEntity> : FilterSpecification<TEntity> where TEntity : class, IPoco, IHasUserName
    {
        private readonly string _username;

        public UsernameSpecification(string username)
        {
            _username = username;
        }

        protected override Expression<Func<TEntity, bool>> SpecificationExpression => p => p.UserName == _username;
    }
}
