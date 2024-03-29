﻿using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.FilterSpecifications.Filters;

namespace DashboardAPI.Models.Builders.Specifications.Category
{
    /// <summary>
    /// Class used to generate <see cref="FilterSpecification{TEntity}"/> for <see cref="Category"/>.
    /// </summary>
    public class CategoryFilterSpecificationBuilder
    {
        private string _inName;
        private int? _minimumPostCount;
        private int? _maximumPostCount;

        public CategoryFilterSpecificationBuilder WithInName(string inName)
        {
            _inName = inName;
            return this;
        }

        public CategoryFilterSpecificationBuilder WithMinimumPostCount(int? minimumPostNumber)
        {
            _minimumPostCount = minimumPostNumber;
            return this;
        }

        public CategoryFilterSpecificationBuilder WithMaximumPostCount(int? maximumPostNumber)
        {
            _maximumPostCount = maximumPostNumber;
            return this;
        }

        /// <summary>
        /// Get filter specification of <see cref="Category"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public FilterSpecification<DashboardDBAccess.Data.Category> Build()
        {
            FilterSpecification<DashboardDBAccess.Data.Category> filter = null;
            if (!string.IsNullOrEmpty(_inName))
                filter = new NameContainsSpecification<DashboardDBAccess.Data.Category>(_inName);
            if (_minimumPostCount != null)
            {
                filter = filter == null
                    ? new MinimumPostCountSpecification<DashboardDBAccess.Data.Category>(_minimumPostCount.Value)
                    : filter & new MinimumPostCountSpecification<DashboardDBAccess.Data.Category>(_minimumPostCount.Value);
            }

            if (_maximumPostCount != null)
            {
                filter = filter == null
                    ? new MaximumPostCountSpecification<DashboardDBAccess.Data.Category>(_maximumPostCount.Value)
                    : filter & new MaximumPostCountSpecification<DashboardDBAccess.Data.Category>(_maximumPostCount.Value);
            }

            return filter;
        }
    }
}
