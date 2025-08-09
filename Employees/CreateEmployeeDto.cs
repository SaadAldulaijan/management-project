namespace ManagementApi.Employees;

public class CreateEmployeeDto
{
    public int PositionId { get; set; }
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
}
