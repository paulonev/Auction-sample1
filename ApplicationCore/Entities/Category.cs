using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        
        private readonly List<Category> _subCategories;
        public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

        public ICollection<Slot> Slots { get; set; }
        
        public Category()
        { }
        
        public Category(Guid id, string name, Category parentCategory = null, List<Category> subCategories = null)
        {
            Id = id;
            Name = name;
            ParentCategory = parentCategory;
            _subCategories = subCategories ?? new List<Category>();
        }

        public void AddCategories(List<Category> categories)
        {
            _subCategories.AddRange(categories);
        }
    }
}