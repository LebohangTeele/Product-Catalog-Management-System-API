#nullable enable
public record Category(
    Guid Id,
    string Name,
    string Description,
    Guid? ParentCategoryId
);