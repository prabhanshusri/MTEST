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
    public class DepartmentController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public DepartmentController(ILoggerService logger, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _logger = logger;
            _wrapper = wrapper;
            _mapper = mapper;
        }



        [HttpPost("RegisterDepartment")]
        public ActionResult<DepartmentDto> RegisterDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto is null)
                {
                    _logger.LogError("Department object is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Department Model object.");
                    return BadRequest("Invalid model object");
                }
                var addDept = _mapper.Map<Department>(departmentDto);
                _wrapper.Department.CreateDepartment(addDept);
                _wrapper.Save();
                var created = _mapper.Map<DepartmentDto>(addDept);
                return created;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RegisterDepartment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment([FromBody] DepartmentDto  departmentDto)
        {
            try
            {
                if (departmentDto is null)
                {
                    _logger.LogError("Department object is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Department Model object.");
                    return BadRequest("Invalid model object");
                }
                var department = _wrapper.Department.GetDepartmentById(departmentDto.DepartmentId);
                if (department is null)
                {
                    _logger.LogError($"Department with id: {departmentDto.DepartmentId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(departmentDto, department);

                _wrapper.Department.UpdateDepartment(department);
                _wrapper.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateDepartment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetDepartmentsList")]
        public IActionResult GetDepartmentsList()
        {
            try
            {
                var departments = _wrapper.Department.FindAll();
                var departmentsResult = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                return Ok(departmentsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetDepartmentsList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
