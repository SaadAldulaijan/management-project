using ManagementApi.Positions;

namespace ManagementApi.Employees;

public static class EmployeeMapper
{
    public static EmployeeDto MapToDto(this Employee entity)
    {
        return new EmployeeDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Phone = entity.Phone,
            Position = new PositionDto
            {
                Id = entity.PositionId,
                Name = entity.Position.Name,
            }
        };
    }
}