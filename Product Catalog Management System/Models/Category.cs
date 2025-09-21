namespace Product_Catalog_Management_System.Models
{

    public record Category(
        Guid Id,
        string Name,
        string Description,
        Guid? ParentCategoryId
    );
}
