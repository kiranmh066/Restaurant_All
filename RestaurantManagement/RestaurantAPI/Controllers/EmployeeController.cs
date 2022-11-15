using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployees")]//
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeService.GetEmployee();
        }



        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            _employeeService.DeleteEmployee(employeeId);
            return Ok("Employee deleted Successfully");
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
            return Ok("Employee Updated Successfully");
        }

        [HttpGet("GetEmployeeById")]
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeService.GetEmployeeById(employeeId);
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employeeInfo)
        {
            _employeeService.AddEmployee(employeeInfo);
            return Ok("Register successfully!!");
        }
        [HttpPost("Login")]
        public string Login([FromBody] Employee employeeInfo)
        {
            Employee Employee = _employeeService.Login(employeeInfo);
            if (Employee != null)
                return Employee.EmpDesignation;
            else
                return "notfound";
        }
    }
}
