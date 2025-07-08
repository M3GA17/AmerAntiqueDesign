using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Primitives;

namespace Domain.ProductManagement;

public class Category : AggregateRoot<IdCategory>
{
    public virtual string Name { get; private set; } = null!;
    public virtual string? Description { get; private set; }
    public virtual bool IsEnabled { get; private set; }
    public virtual IdUser IdUserCreate { get; private set; } = null!;
    public virtual IdUser? IdUserUpdate { get; private set; }

    public Category? CategoryParent { get; private set; }
    public ICollection<Category> SubCategories { get; private set; } = [];

    public Category() : base(new IdCategory())
    {
        IsEnabled = true;
    }

    public static Category Create(string name, string? description, Category? categoryParent, IdUser idUserCreate, DateTimeOffset dateCreate)
    {
        var category = new Category
        {
            Name = name,
            Description = description,
            CategoryParent = categoryParent,
            IdUserCreate = idUserCreate,
            DateCreate = dateCreate,
        };

        return category;
    }

    public void Update(string name, string description, Category? categoryParent, bool isEnabled, IdUser idUserUpdate, DateTimeOffset dateUpdate)
    {
        Name = name;
        Description = description;
        CategoryParent = categoryParent;
        IdUserUpdate = idUserUpdate;
        IsEnabled = isEnabled;
        DateUpdate = dateUpdate;
    }

    public void SetIsEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}
