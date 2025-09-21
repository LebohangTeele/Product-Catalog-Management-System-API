using Product_Catalog_Management_System.Models;

namespace Product_Catalog_Management_System.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryNode
    {
        public Category Category { get; set; } = default!;
        public List<CategoryNode> Children { get; set; } = new();
    }

    public static class CategoryTreeBuilder
    {
        public static List<CategoryNode> BuildTree(IEnumerable<Category> categories)
        {
            var lookup = categories.ToLookup(c => c.ParentCategoryId);
            List<CategoryNode> Build(Guid? parentId)
            {
                return lookup[parentId]
                    .Select(cat => new CategoryNode
                    {
                        Category = cat,
                        Children = Build(cat.Id)
                    }).ToList();
            }
            return Build(null);
        }
    }
}
