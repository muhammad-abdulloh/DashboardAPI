﻿using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.FilterSpecifications.Filters;

namespace DashboardAPI.Models.Builders.Specifications.Comment
{
    /// <summary>
    /// Class used to generate <see cref="FilterSpecification{TEntity}"/> for <see cref="Comment"/>.
    /// </summary>
    public class CommentFilterSpecificationBuilder
    {
        private string _inAuthorUserName;
        private string _inPostParentName;
        private string _inContent;

        public CommentFilterSpecificationBuilder WithInAuthorUserName(string inAuthorUserName)
        {
            _inAuthorUserName = inAuthorUserName;
            return this;
        }

        public CommentFilterSpecificationBuilder WithPostParentName(string inPostParentName)
        {
            _inPostParentName = inPostParentName;
            return this;
        }

        public CommentFilterSpecificationBuilder WithInContent(string inContent)
        {
            _inContent = inContent;
            return this;
        }

        /// <summary>
        /// Get filter specification of <see cref="Comment"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public FilterSpecification<DashboardDBAccess.Data.Comment> Build()
        {
            FilterSpecification<DashboardDBAccess.Data.Comment> filter = null;
            if (!string.IsNullOrEmpty(_inAuthorUserName))
                filter = new AuthorUsernameContainsSpecification<DashboardDBAccess.Data.Comment>(_inAuthorUserName);
            if (!string.IsNullOrEmpty(_inPostParentName))
            {
                filter = filter == null
                    ? new PostParentNameContainsSpecification<DashboardDBAccess.Data.Comment>(_inPostParentName)
                    : filter & new PostParentNameContainsSpecification<DashboardDBAccess.Data.Comment>(_inPostParentName);
            }
            if (!string.IsNullOrEmpty(_inContent))
            {
                filter = filter == null
                    ? new ContentContainsSpecification<DashboardDBAccess.Data.Comment>(_inContent)
                    : filter & new PostParentNameContainsSpecification<DashboardDBAccess.Data.Comment>(_inContent);
            }

            return filter;
        }
    }
}
