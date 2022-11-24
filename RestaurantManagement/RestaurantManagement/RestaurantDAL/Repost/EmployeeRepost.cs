using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class EmployeeRepost : IEmployeeRepost
    {
        RestaurantDbContext _dbContext;//default private

        public EmployeeRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddEmployee(Employee employee)
        {
            _dbContext.tbl_Employee.Add(employee);
            _dbContext.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.tbl_Employee.Find(employeeId);
            _dbContext.tbl_Employee.Remove(employee);
            _dbContext.SaveChanges();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _dbContext.tbl_Employee.Find(employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbContext.tbl_Employee.ToList();
        }

        public Employee Login(Employee employee)
        {
            Employee employeeinfo = null;
            var result = _dbContext.tbl_Employee.Where(obj => obj.EmpEmail == employee.EmpEmail && obj.EmpPassword == employee.EmpPassword).ToList();
            if (result.Count > 0)
            {
                employeeinfo = result[0];
            }
            return employeeinfo;
        }

        public void UpdateEmployee(Employee employee)
        {

            _dbContext.Entry(employee).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
