using DashboardDBAccess.Specifications.FilterSpecifications.Filters;
using System.Linq.Expressions;
using System;
using DashboardDBAccess.Data;
using Xunit;

namespace DashboardDBAccess.Tests.SpecificationTests.Filters
{
    public class EmailEqualsSpecificationTests
    {
        [Fact]
        public void CanCheckValidMatchItem()
        {
            // Arrange
            var specification = new EmailEqualsSpecification<User>("water.test@email.com");
            var function = ((Expression<Func<User, bool>>)specification).Compile();

            // Act
            var matchSpecification = function.Invoke(new User { Email = "water.test@email.com" });

            // Assert
            Assert.True(matchSpecification);
        }

        [Theory]
        [InlineData("lava.test@email.com")]
        [InlineData("water.test@email")]
        [InlineData("water-test@email.com")]
        [InlineData("water.test@email.net")]
        public void CanCheckInvalidMatchItem(string email)
        {
            // Arrange
            var specification = new EmailEqualsSpecification<User>("water.test@email.com");
            var function = ((Expression<Func<User, bool>>)specification).Compile();

            // Act
            var matchSpecification = function.Invoke(new User { Email = email });

            // Assert
            Assert.False(matchSpecification);
        }
    }
}
