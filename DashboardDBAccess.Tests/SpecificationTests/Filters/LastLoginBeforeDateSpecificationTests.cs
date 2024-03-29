﻿using DashboardDBAccess.Specifications.FilterSpecifications.Filters;
using System.Linq.Expressions;
using System;
using DashboardDBAccess.Data;
using Xunit;

namespace DashboardDBAccess.Tests.SpecificationTests.Filters
{
    public class LastLoginBeforeDateSpecificationTests
    {
        [Theory]
        [InlineData("2024-01-01 0:00:00Z")]
        [InlineData("2023-05-27 9:15:11Z")]
        public void CanCheckValidMatchItem(DateTimeOffset lastLoginSpecification)
        {
            // Arrange
            var specification = new LastLoginBeforeDateSpecification<User>(lastLoginSpecification);
            var function = ((Expression<Func<User, bool>>)specification).Compile();
            var lastLoginOfUser = new DateTimeOffset(new DateTime(2023, 05, 27, 9, 15, 10, DateTimeKind.Utc));

            // Act
            var matchSpecification = function.Invoke(new User { LastLogin = lastLoginOfUser });

            // Assert
            Assert.True(matchSpecification);
        }

        [Theory]
        [InlineData("2022-01-01 0:00:00Z")]
        [InlineData("2023-05-27 9:15:10Z")]
        [InlineData("2023-05-27 9:15:09Z")]
        public void CanCheckInvalidMatchItem(DateTimeOffset lastLoginSpecification)
        {
            // Arrange
            var specification = new LastLoginBeforeDateSpecification<User>(lastLoginSpecification);
            var function = ((Expression<Func<User, bool>>)specification).Compile();
            var lastLoginOfUser = new DateTimeOffset(new DateTime(2023, 05, 27, 9, 15, 10, DateTimeKind.Utc));

            // Act
            var matchSpecification = function.Invoke(new User() { LastLogin = lastLoginOfUser });

            // Assert
            Assert.False(matchSpecification);
        }
    }
}
