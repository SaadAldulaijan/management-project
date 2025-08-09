namespace ManagementApi.OrganizationUnits;

public class OrganizationUnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string FullPath { get; set; } = default!;

    public List<OrganizationUnitDto> Children { get; set; } = new();
}


