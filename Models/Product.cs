#nullable enable
public record Product(
    Guid Id,
    string Name,
    string Description,
    string SKU,
    decimal Price,
    int Quantity,
    Guid CategoryId,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IComparable<Product>
{
    public int CompareTo(Product? other)
    {
        if (other is null) return 1;
        // Example: sort by Name, then Price
        int nameCompare = string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        return nameCompare != 0 ? nameCompare : Price.CompareTo(other.Price);
    }
}