﻿using System;
using DashboardDBAccess.Data;
using DashboardDBAccess.Repositories.Category;
using DashboardDBAccess.Repositories.UnitOfWork;

namespace DashboardDBAccess.Tests.Builders
{
    public class CategoryBuilder
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private string _name;

        public CategoryBuilder(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public CategoryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Category Build()
        {
            var testCategory = new Category()
            {
                Name = Guid.NewGuid().ToString()
            };
            if (!string.IsNullOrEmpty(_name))
                testCategory.Name = _name;
            _categoryRepository.Add(testCategory);
            _unitOfWork.Save();
            return testCategory;
        }
    }
}
