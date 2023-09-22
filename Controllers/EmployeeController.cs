using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTest.DTO;
using MTest.Entities;
using MTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public EmployeeController(ILoggerService logger, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _logger = logger;
            _wrapper = wrapper;
            _mapper = mapper;
        }



        [HttpPost("RegisterEmployee")]
        public ActionResult<EmployeeDto> RegisterEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                {
                    _logger.LogError("Employee object is null.");
                    return BadRequest("Employee object is null.");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee model object");
                    return BadRequest("Invalid model object");
                }
                var addEmployee = _mapper.Map<Employee>(employeeDto);
                _wrapper.Employee.CreateEmployee(addEmployee);
                _wrapper.Save();
                var created = _mapper.Map<EmployeeDto>(addEmployee);
                return created;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RegisterEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto is null)
                {
                    _logger.LogError("Employee object is null.");
                    return BadRequest("Employee object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Employee Model object.");
                    return BadRequest("Invalid model object");
                }
                var Employee = _wrapper.Employee.GetEmployeeById(employeeDto.EmployeeId);
                if (Employee is null)
                {
                    _logger.LogError($"Employee with id: {employeeDto.EmployeeId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(employeeDto, Employee);

                _wrapper.Employee.UpdateEmployee(Employee);
                _wrapper.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetEmployeesList")]
        public IActionResult GetEmployeesList()
        {
            try
            {
                var employees = _wrapper.Employee.GetAllEmployees();
                var employeesResult = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return Ok(employeesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeesList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("CustomGetEmployees")]
        public IActionResult CustomGetEmployees()
        {
            try
            {
                var todaysDate = DateTime.Now.Date;
                var employees = _wrapper.Employee.GetCustomList()
                                                 .Select(x => new { 
                                                                   EmployeeCode=x.EmployeeCode, 
                                                                   EmployeeName=x.Name, 
                                                                   Department=x.Department.Name,
                                                                   Tenure =$"{todaysDate.Subtract(x.DateofJoining).Days/365}.{(todaysDate.Subtract(x.DateofJoining).Days%365)/31} years"  
                                                                   });

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeesList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
