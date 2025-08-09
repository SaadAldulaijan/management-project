namespace ManagementApi.OrganizationUnits;

public class OrganizationUnit
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string FullPath { get; set; } = default!;

    public Guid? ParentId { get; set; }

    public OrganizationUnit? Parent { get; set; }

    public ICollection<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();

    public virtual ICollection<OrganizationUnitEmployee> OrganizationUnitEmployees { get; set; } = [];


    private OrganizationUnit() { }

    public OrganizationUnit(string name, string? fullPath = null)
    {
        Name = name;
    }


    public void AddChild(OrganizationUnit child)
    {
        child.Parent = this;
        child.ParentId = Id;

        // Determine child index (1-based)
        int siblingIndex = Children.Count + 1;

        child.FullPath = string.IsNullOrEmpty(FullPath)
            ? $"{siblingIndex}"
            : $"{FullPath}.{siblingIndex}";

        Children.Add(child);
    }


    public List<OrganizationUnit> GetAncestors()
    {
        var ancestors = new List<OrganizationUnit>();
        var current = Parent;

        while (current != null)
        {
            ancestors.Add(current);
            current = current.Parent;
        }

        return ancestors;
    }


    // Traverse down the hierarchy (descendants)
    public List<OrganizationUnit> GetDescendants()
    {
        var descendants = new List<OrganizationUnit>();
        foreach (var child in Children)
        {
            descendants.Add(child);
            descendants.AddRange(child.GetDescendants());
        }

        return descendants;
    }


    // Get the full path (e.g., ou1 > ou1.2 > ou1.2.1)
    public string GetFullPath(string separator = ".")
    {
        var ancestors = GetAncestors();
        ancestors.Reverse(); // So root is first
        var names = ancestors.Select(a => a.Name).ToList();
        names.Add(Name);
        return string.Join(separator, names);
    }


    public List<OrganizationUnit> GetSubtree()
    {
        var result = new List<OrganizationUnit> { this };
        foreach (var child in Children)
        {
            result.AddRange(child.GetSubtree());
        }
        return result;
    }
}
