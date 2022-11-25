using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class EmployeeService
    {
        IEmployeeRepost _employeeRepository;


        //Unable to resolve ====>>>> Object issues
        public EmployeeService(IEmployeeRepost employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        //Update Employee
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }

        //Delete Employee
        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
        }

        //Get EmployeeByID
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        //Get Employees
        public IEnumerable<Employee> GetEmployee()
        {
            return _employeeRepository.GetEmployees();
        }

        //Registering Employee
        public void AddEmployee(Employee employeeInfo)
        {
            _employeeRepository.AddEmployee(employeeInfo);
        }

        //Logging Employee
        public Employee Login(Employee employeeInfo)
        {
            return _employeeRepository.Login(employeeInfo);
        }
    }
}
