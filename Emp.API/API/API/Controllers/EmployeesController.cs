using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly EmpDbContext _db;

        public EmployeesController(EmpDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee emp)
        {
            emp.Id = Guid.NewGuid();
            await _db.Employees.AddAsync(emp);
            await _db.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(p => p.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee employeeUpdateRequest)
        {
            var employee =await _db.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = employeeUpdateRequest.Name;
            employee.Email = employeeUpdateRequest.Email;
            employee.Phone = employeeUpdateRequest.Phone;
            employee.Salary = employeeUpdateRequest.Salary;
            employee.Department = employeeUpdateRequest.Department;

            await _db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
