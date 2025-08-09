using ManagementApi.Employees;

namespace ManagementApi.OrganizationUnits;

public class OrganizationUnitEmployee
{
    public Guid OrganizationUnitId { get; set; }
    public OrganizationUnit OrganizationUnit { get; set; } = default!;

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
}

