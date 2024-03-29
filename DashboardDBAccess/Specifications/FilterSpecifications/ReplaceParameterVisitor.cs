﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DashboardDBAccess.Specifications.FilterSpecifications
{
    /// <summary>
    /// This class is a component of <see cref="FilterSpecification{T}"/>
    /// See https://dotnetfalcon.com/using-the-specification-pattern-with-repository-and-unit-of-work/
    /// </summary>
    internal class ReplaceParameterVisitor : ExpressionVisitor, IEnumerable<KeyValuePair<ParameterExpression, ParameterExpression>>
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterMappings = new();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameterMappings.TryGetValue(node, out var newValue) ? newValue : node;
        }

        public void Add(ParameterExpression parameterToReplace, ParameterExpression replaceWith) => _parameterMappings.Add(parameterToReplace, replaceWith);

        public IEnumerator<KeyValuePair<ParameterExpression, ParameterExpression>> GetEnumerator() => _parameterMappings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
