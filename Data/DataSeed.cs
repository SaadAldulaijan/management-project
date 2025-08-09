using ManagementApi.Employees;
using ManagementApi.OrganizationUnits;
using ManagementApi.Positions;

namespace ManagementApi.Data;
public static class DataSeed
{
    public static List<Employee> SeedEmployees()
    {
        return [
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Saad",
                Phone = "1234567890",
                PositionId = 1,
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Ahmed",
                Phone = "2211221122",
                PositionId = 2
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Ali",
                Phone = "0566026402",
                PositionId = 3,
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Meshari",
                Phone = "9922112233",
                PositionId = 4
            }
        ];
    }

    public static List<Position> SeedPositions()
    {
        return [
            new() {
                Id = 1,
                Name = "Software Expert"
            },
            new() {
                Id = 2,
                Name = "Fullstack Developer"
            },
            new() {
                Id = 3,
                Name = "Application Specialist"
            }
            ,new() {
                Id = 4,
                Name = "Manager"
            },
        ];
    }


    public static List<OrganizationUnit> SeedOrganizationUnits()
    {
        var root = new OrganizationUnit("root") { FullPath = "0" }; // root

        var it = new OrganizationUnit("Information Technology");
        var hr = new OrganizationUnit("Human Resources");
        var finance = new OrganizationUnit("Finance");
        root.AddChild(it);
        root.AddChild(hr);
        root.AddChild(finance);

        var application = new OrganizationUnit("Application");
        var infra = new OrganizationUnit("Infrastructure");
        it.AddChild(application);
        it.AddChild(infra);

        var dev = new OrganizationUnit("Application Development");
        var ops = new OrganizationUnit("Application Operation");
        var devops = new OrganizationUnit("DevOps");

        application.AddChild(dev);
        application.AddChild(ops);
        application.AddChild(devops);

        var recruitment = new OrganizationUnit("Recruitment");
        var employeeServices = new OrganizationUnit("Employee Services");
        hr.AddChild(recruitment);
        hr.AddChild(employeeServices);

        var treasury = new OrganizationUnit("Treasury");
        finance.AddChild(treasury);


        var allUnits = root.GetSubtree();

        return allUnits;
    }
}
