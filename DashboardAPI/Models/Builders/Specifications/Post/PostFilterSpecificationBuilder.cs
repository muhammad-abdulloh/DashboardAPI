using System;
using System.Collections.Generic;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.FilterSpecifications.Filters;

namespace DashboardAPI.Models.Builders.Specifications.Post
{
    /// <summary>
    /// Class used to generate <see cref="FilterSpecification{TEntity}"/> for <see cref="Post"/>.
    /// </summary>
    public class PostFilterSpecificationBuilder
    {

        private string _inName;
        private string _inContent;
        private DateTimeOffset? _toPublishedAt;
        private DateTimeOffset? _fromPublishedAt;
        private int? _minimumLikeCount;
        private int? _maximumLikeCount;
        private List<string> _tags;

        public PostFilterSpecificationBuilder WithInContent(string inContent)
        {
            _inContent = inContent;
            return this;
        }

        public PostFilterSpecificationBuilder WithInName(string inName)
        {
            _inName = inName;
            return this;
        }

        public PostFilterSpecificationBuilder WithToPublishedAt(DateTimeOffset? toPublishedAt)
        {
            _toPublishedAt = toPublishedAt;
            return this;
        }

        public PostFilterSpecificationBuilder WithFromPublishedAt(DateTimeOffset? fromPublishedAt)
        {
            _fromPublishedAt = fromPublishedAt;
            return this;
        }

        public PostFilterSpecificationBuilder WithMinimumLikeCount(int? minimumLikeCount)
        {
            _minimumLikeCount = minimumLikeCount;
            return this;
        }

        public PostFilterSpecificationBuilder WithMaximumLikeCount(int? maximumLikeCount)
        {
            _maximumLikeCount = maximumLikeCount;
            return this;
        }

        public PostFilterSpecificationBuilder WithTags(List<string> tags)
        {
            _tags = tags;
            return this;
        }

        /// <summary>
        /// Get filter specification of <see cref="Post"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public FilterSpecification<DashboardDBAccess.Data.Post> Build()
        {

            FilterSpecification<DashboardDBAccess.Data.Post> filter = null;

            if (_inContent != null)
                filter = new ContentContainsSpecification<DashboardDBAccess.Data.Post>(_inContent);
            if (_inName != null)
            {
                filter = filter == null ?
                    new NameContainsSpecification<DashboardDBAccess.Data.Post>(_inName)
                    : filter & new NameContainsSpecification<DashboardDBAccess.Data.Post>(_inName);
            }
            if (_toPublishedAt != null)
            {
                filter = filter == null ?
                    new PublishedBeforeDateSpecification<DashboardDBAccess.Data.Post>(_toPublishedAt.Value)
                    : filter & new PublishedBeforeDateSpecification<DashboardDBAccess.Data.Post>(_toPublishedAt.Value);
            }
            if (_fromPublishedAt != null)
            {
                filter = filter == null ?
                    new PublishedAfterDateSpecification<DashboardDBAccess.Data.Post>(_fromPublishedAt.Value)
                    : filter & new PublishedAfterDateSpecification<DashboardDBAccess.Data.Post>(_fromPublishedAt.Value);
            }
            if (_minimumLikeCount != null)
            {
                filter = filter == null ?
                    new MinimumLikeCountSpecification<DashboardDBAccess.Data.Post>(_minimumLikeCount.Value)
                    : filter & new MinimumLikeCountSpecification<DashboardDBAccess.Data.Post>(_minimumLikeCount.Value);
            }
            if (_maximumLikeCount != null)
            {
                filter = filter == null ?
                    new MaximumLikeCountSpecification<DashboardDBAccess.Data.Post>(_maximumLikeCount.Value)
                    : filter & new MaximumLikeCountSpecification<DashboardDBAccess.Data.Post>(_maximumLikeCount.Value);
            }
            if (_tags != null)
            {
                foreach (var tag in _tags)
                {
                    filter = filter == null
                        ? new TagSpecification<DashboardDBAccess.Data.Post>(tag)
                        : filter & new TagSpecification<DashboardDBAccess.Data.Post>(tag);
                }
            }

            return filter;
        }
    }
}
