namespace Product_Catalog_Management_System.Extensions
{
    using Product_Catalog_Management_System.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ProductFilterExtensions
    {
        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> products, Guid categoryId)
            => products.Where(p => p.CategoryId == categoryId);

        public static IEnumerable<Product> SearchByName(this IEnumerable<Product> products, string name)
            => products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

}
