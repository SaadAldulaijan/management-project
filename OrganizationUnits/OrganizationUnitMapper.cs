namespace ManagementApi.OrganizationUnits;

public static class OrganizationUnitMapper
{
    public static OrganizationUnitDto MapToDto(OrganizationUnit unit)
    {
        return new OrganizationUnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            FullPath = unit.FullPath,
            Children = unit.Children.Select(MapToDto).ToList()
        };
    }
}