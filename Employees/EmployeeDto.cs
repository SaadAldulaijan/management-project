using ManagementApi.Positions;

namespace ManagementApi.Employees;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;

// position dto
    public PositionDto Position { get; set; } = default!;

}
