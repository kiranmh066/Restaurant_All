using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantAPI.Controllers;
using RestaurantBLL.Services;
using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using AutoFixture;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class EmployeeControllerTests
    {
        EmployeeController employeeController;
        Fixture _fixture;
        Mock<IEmployeeRepost> moq;
        public EmployeeControllerTests()
        {
            _fixture = new Fixture();
            moq = new Mock<IEmployeeRepost>();
        }
        [TestInitialize]
        public void setup()
        {
            var moq = new Mock<EmployeeService>();
        }
        [TestMethod()]
        public async Task GetEmployeesTest()
        {
            var employeelist = _fixture.CreateMany<Employee>(3).ToList();

            moq.Setup(x => x.GetEmployees()).Returns(employeelist);
            employeeController = new EmployeeController(new EmployeeService(moq.Object));

            var result = employeeController.GetEmployees();

            Assert.AreEqual(result.Count(), 3);
        }
        [TestMethod()]
        public async Task GetEmployeesNegativeTest()
        {
            List<Employee> employeelist = null;

            moq.Setup(x => x.GetEmployees()).Returns(employeelist);
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            Assert.IsNull(employeeController.GetEmployees());

        }
        [TestMethod()]
        public void DeleteEmployeeTest()
        {
            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.DeleteEmployee(1));

            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            var result = employeeController.DeleteEmployee(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 200);
        }
        [TestMethod()]
        public void DeleteEmployee_ThrowsException_IfIdNotFound()
        {
            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.DeleteEmployee(It.IsAny<int>())).
                 Throws(new Exception());
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            var result = employeeController.DeleteEmployee(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void UpdateEmployeeTest()
        {
            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.UpdateEmployee(employee));
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            var result = employeeController.UpdateEmployee(employee);
            var Obj = result as ObjectResult;

            Assert.AreEqual(200, Obj.StatusCode);
        }
        [TestMethod()]
        public void UpdateEmployee_ThrowsException_IfIdNotFound()
        {
            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.UpdateEmployee(It.IsAny<Employee>())).
                 Throws(new Exception());
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            var result = employeeController.UpdateEmployee(employee);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void GetEmployeeByIdTest()
        {
            var employee = _fixture.Create<Employee>();


            moq.Setup(x => x.GetEmployeeById(1)).Returns(employee);
            employeeController = new EmployeeController(new EmployeeService(moq.Object));

            Assert.AreEqual(employeeController.GetEmployeeById(1), employee);
        }
        [TestMethod()]
        public void GetEmployeeById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();


            moq.Setup(x => x.GetEmployeeById(10)).Returns(employee);
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            // Act
            var okResult = employeeController.GetEmployeeById(1);
            // Assert
            Assert.AreEqual(okResult, null);
        }
        [TestMethod()]
        public async Task AddEmployeeTest()
        {
            var employee = _fixture.Create<Employee>();


            moq.Setup(x => x.AddEmployee(employee));

            employeeController = new EmployeeController(new EmployeeService(moq.Object));

            var result = employeeController.AddEmployee(employee);
            var Obj = result as ObjectResult;

            Assert.AreEqual(200, Obj.StatusCode);
        }
        [TestMethod()]
        public async Task AddEmployeeNegativeTest()
        {
            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.AddEmployee(It.IsAny<Employee>())).
                 Throws(new Exception());
            employeeController = new EmployeeController(new EmployeeService(moq.Object));
            var result = employeeController.AddEmployee(employee);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void LoginTest()
        {
            var employee1 = _fixture.Create<Employee>();
            moq.Setup(x => x.Login(employee1)).Returns(employee1);
            employeeController = new EmployeeController(new EmployeeService(moq.Object));

            var result = employeeController.Login(employee1);

            Assert.AreEqual(employee1, result);
        }
        [TestMethod()]
        public void LoginFailTest()
        {

            var employee = _fixture.Create<Employee>();

            moq.Setup(x => x.Login(It.IsAny<Employee>())).
              Throws(new Exception());
            employeeController = new EmployeeController(new EmployeeService(moq.Object));

            var result = employeeController.Login(employee);

            Assert.IsNull(result);
        }
    }
}