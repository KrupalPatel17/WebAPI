using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. Get all countries
    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        return await _context.Countries.ToListAsync();
    }

    // 2. Get states based on country_id
    [HttpGet("states/{countryId}")]
    public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountry(int countryId)
    {
        var states = await _context.States.Where(s => s.CountryId == countryId).ToListAsync();
        if (!states.Any()) return NotFound("No states found for the given country.");
        return states;
    }

    // 3. Get cities based on state_id
    [HttpGet("cities/{stateId}")]
    public async Task<ActionResult<IEnumerable<City>>> GetCitiesByState(int stateId)
    {
        var cities = await _context.Cities.Where(c => c.StateId == stateId).ToListAsync();
        if (!cities.Any()) return NotFound("No cities found for the given state.");
        return cities;
    }

    // 4. Get all departments (id & name)
    [HttpGet("departments")]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }

    // 5. Get all employees
    [HttpGet("employees")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Country)
            .Include(e => e.State)
            .Include(e => e.City)
            .ToListAsync();
    }

    // 6. Insert employee (only required fields)
   [HttpPost("employees")]
public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
{
    // Validate foreign key references
    if (!await _context.Departments.AnyAsync(d => d.Id == employee.DepartmentId) ||
        !await _context.Countries.AnyAsync(c => c.Id == employee.CountryId) ||
        !await _context.States.AnyAsync(s => s.Id == employee.StateId) ||
        !await _context.Cities.AnyAsync(c => c.Id == employee.CityId))
    {
        return BadRequest("Invalid DepartmentId, CountryId, StateId, or CityId.");
    }

    // Only insert required fields
    var newEmployee = new Employee
    {
        Name = employee.Name,
        Email = employee.Email,
        DepartmentId = employee.DepartmentId,
        CountryId = employee.CountryId,
        StateId = employee.StateId,
        CityId = employee.CityId
    };

    _context.Employees.Add(newEmployee);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetEmployees), new { id = newEmployee.Id }, new
    {
        newEmployee.Id,
        newEmployee.Name,
        newEmployee.Email,
        newEmployee.DepartmentId,
        newEmployee.CountryId,
        newEmployee.StateId,
        newEmployee.CityId
    });
}

    // 7. Update employee by ID
    [HttpPut("employees/{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest("Employee ID mismatch.");
        }

        var existingEmployee = await _context.Employees.FindAsync(id);
        if (existingEmployee == null)
        {
            return NotFound("Employee not found.");
        }

        // Validate foreign keys
        if (!await _context.Departments.AnyAsync(d => d.Id == employee.DepartmentId) ||
            !await _context.Countries.AnyAsync(c => c.Id == employee.CountryId) ||
            !await _context.States.AnyAsync(s => s.Id == employee.StateId) ||
            !await _context.Cities.AnyAsync(c => c.Id == employee.CityId))
        {
            return BadRequest("Invalid DepartmentId, CountryId, StateId, or CityId.");
        }

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.DepartmentId = employee.DepartmentId;
        existingEmployee.CountryId = employee.CountryId;
        existingEmployee.StateId = employee.StateId;
        existingEmployee.CityId = employee.CityId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // 8. Delete employee by ID
    [HttpDelete("employees/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // 9. Search employees by department_id, city_id, state_id, or country_id
    // 9. Search employees
    [HttpGet("employees/search")]
    public async Task<IActionResult> SearchEmployees(int? departmentId, int? cityId, int? stateId, int? countryId)
    {
        var query = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Country)
            .Include(e => e.State)
            .Include(e => e.City)
            .AsQueryable();

        // Apply filters if values are provided
        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);
        if (cityId.HasValue)
            query = query.Where(e => e.CityId == cityId.Value);
        if (stateId.HasValue)
            query = query.Where(e => e.StateId == stateId.Value);
        if (countryId.HasValue)
            query = query.Where(e => e.CountryId == countryId.Value);

        var result = await query
            .Select(e => new
            {
                e.Id,
                e.Name,
                e.Email,
                e.DepartmentId,
                e.CountryId,
                e.StateId,
                e.CityId,
                Department = e.Department.Name,
                Country = e.Country.Name,
                State = e.State.Name,
                City = e.City.Name
            })
            .ToListAsync();

        if (!result.Any())
            return NotFound("No employees found matching the criteria.");

        return Ok(result);
    }

}
