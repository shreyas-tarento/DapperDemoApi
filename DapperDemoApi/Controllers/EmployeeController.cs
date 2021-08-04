using DapperDemoApi.IServices;
using DapperDemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var exists = await _employeeService.GetEmployee(id);
            if (exists == null)
              return  NotFound($"Employee for id #{id} not found");

            return Ok(exists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            await _employeeService.CreateEmployee(employee);
            return CreatedAtAction("GetEmployee", new { Id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody]Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("UserId mismatch");

            var existing = await _employeeService.GetEmployee(id);
            if (existing == null)
                return NotFound($"Employee for id:#{id} not found");

            return Ok(await _employeeService.UpdateEmployee(existing, employee));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var existing = await  _employeeService.GetEmployee(id);
            if (existing == null)
                return NotFound($"Employee for id:#{id} not found");

            return Ok(await _employeeService.DeleteEmployee(existing));
        }


    }
}
