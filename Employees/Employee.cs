using ManagementApi.OrganizationUnits;
using ManagementApi.Positions;

namespace ManagementApi.Employees;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int PositionId { get; set; }
    public Position Position { get; set; } = default!;
    public string Phone { get; set; } = default!;


    //public virtual ICollection<OrganizationUnitEmployee> OrganizationUnitEmployees { get; set; } = [];


}
