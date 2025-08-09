using ManagementApi.Data;
using ManagementApi.OrganizationUnits;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Controllers;

[Route("api/ous")]
[ApiController]
public class OrganizationUnitsController : ControllerBase
{

    private readonly DataContext _cxt;

    public OrganizationUnitsController(DataContext cxt)
    {
        _cxt = cxt;
    }


    [HttpPost("assign")]
    public async Task<ActionResult> AssignEmployeeToOrganizationUnit(AssignEmployeeToOrganizationUnitDto input) 
    {

        var employee = await _cxt.Employees.FindAsync(input.EmployeeId);
        if (employee == null)
            return NotFound("employee id not found");

        var ou = await _cxt.OrganizationUnits.FindAsync(input.OrganizationUnitId);
        if (ou == null)
            return NotFound("ou id not found");


        OrganizationUnitEmployee organizationUnitEmployee = new OrganizationUnitEmployee
        {
            OrganizationUnitId = input.OrganizationUnitId,
            EmployeeId = employee.Id,
        };

        await _cxt.OrganizationUnitEmployees.AddAsync(organizationUnitEmployee);

        await _cxt.SaveChangesAsync();

        return Created();
    }


    [HttpGet("members/{id}")]
    public async Task<ActionResult> GetMembersAsync(Guid id)
    {
        var employees = await _cxt.OrganizationUnitEmployees
            .Include(x => x.Employee)
            .Where(x => x.OrganizationUnitId == id)
            .Select(x => x.Employee)    
            .ToListAsync();



        return Ok(employees);
    } 

    [HttpGet("lookup")]
    public async Task<ActionResult> GetLookup()
    {
        var ous = await _cxt.OrganizationUnits
            .Select(x => new OrganizationUnitLookupDto(x.Id,x.Name))
            .ToListAsync();

        return Ok(ous);
    }

    [HttpGet("tree")]
    public async Task<ActionResult> GetTreeAsync()
    {

        var allUnits = await _cxt.OrganizationUnits.ToListAsync();

        var ouLookup = allUnits.ToLookup(x => x.ParentId);

        OrganizationUnit? BuildTree(Guid? parentId)
        {
            var nodes = ouLookup[parentId];

            foreach (var node in nodes)
            {
                node.Children = ouLookup[node.Id].ToList();
                foreach (var child in node.Children)
                {
                    BuildTree(child.Id);
                }
            }

            return nodes.FirstOrDefault();
        }

        var root = BuildTree(null);



        if (root == null)
            return NotFound();

        var dto = OrganizationUnitMapper.MapToDto(root);
        return Ok(dto);
    }
}



public class AssignEmployeeToOrganizationUnitDto
{
    public Guid EmployeeId { get; set; }
    public Guid OrganizationUnitId { get; set; }
}


public record OrganizationUnitLookupDto(Guid Id, string Name);