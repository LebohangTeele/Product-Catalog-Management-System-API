public class ProductSearchEngine
{
    private readonly IRepository<Product> _productRepo;
    private readonly Dictionary<string, List<Product>> _searchCache = new();

    public ProductSearchEngine(IRepository<Product> productRepo)
    {
        _productRepo = productRepo;
    }

    public IEnumerable<Product> Search(string name, Guid? categoryId)
    {
        var cacheKey = $"{name}:{categoryId}";
        if (_searchCache.TryGetValue(cacheKey, out var cached))
            return cached;

        var products = _productRepo.GetAll();
        if (!string.IsNullOrWhiteSpace(name))
            products = products.SearchByName(name);
        if (categoryId.HasValue)
            products = products.FilterByCategory(categoryId.Value);

        var result = products.ToList();
        _searchCache[cacheKey] = result;
        return result;
    }
}