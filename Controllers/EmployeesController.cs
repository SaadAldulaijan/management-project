using ManagementApi.Data;
using ManagementApi.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly DataContext _ctx;

    public EmployeesController(DataContext ctx)
    {
        _ctx = ctx;
    }



    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetListAsync()
    {
        var employees = await _ctx.Employees
            .Include(x => x.Position)
            .Select(x => x.MapToDto())
            .ToListAsync();


        return Ok(employees);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetAsync(Guid id)
    {
        var employee = await _ctx.Employees.Include(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == id);

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateAsync(CreateEmployeeDto input)
    {
        var employee = new Employee
        {
            Name = input.Name,
            Phone = input.Phone,
            PositionId = input.PositionId,
        };

        await _ctx.Employees.AddAsync(employee);
        await _ctx.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var employee = await _ctx.Employees.FindAsync(id);

        if (employee is null) throw new ArgumentNullException(nameof(employee));


        _ctx.Employees.Remove(employee);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }

}
