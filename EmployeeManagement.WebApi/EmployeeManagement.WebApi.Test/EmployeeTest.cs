using EmployeeManagement.WebApi.Controllers;
using EmployeeManagement.WebApi.IServices;
using EmployeeManagement.WebApi.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmployeeManagement.WebApi.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void TestGetEmployees()
        {

            var mockRepo = new Mock<IEmployeeService>();
            var mockIdentity = new Mock<IIdentityService>();
            mockRepo.Setup(repo => repo.GetEmployee())
                .Returns(GetTestEmployees());
            var controller = new EmployeeController(mockRepo.Object, mockIdentity.Object);
            // Act
            var result = controller.GetEmployee();
            // Assert
            Assert.Equal(1,result.Count());
        }

        [Fact]
        public void TestAddEmployees()
        {

            var mockRepo = new Mock<IEmployeeService>();
            var mockIdentity = new Mock<IIdentityService>();
            mockRepo.Setup(repo => repo.AddEmployee(It.IsAny<Employee>()))
      .Verifiable();
            var controller = new EmployeeController(mockRepo.Object, mockIdentity.Object);
            // Act
            var emp = GetTestEmployees();
            var addEmp = controller.AddEmployee(emp.First());
            // Assert
           // Assert.IsType<OkObjectResult>(addEmp);
            mockRepo.Verify();
        }

        [Fact]
        public void TestEditEmployees()
        {

            var mockRepo = new Mock<IEmployeeService>();
            var mockIdentity = new Mock<IIdentityService>();
            mockRepo.Setup(repo => repo.UpdateEmployee(It.IsAny<Employee>()))
      .Verifiable();
            var controller = new EmployeeController(mockRepo.Object, mockIdentity.Object);
            // Act
            var emp = GetTestEmployees();
            var updateEmp = controller.UpdateEmployee(emp.First());
            // Assert
            //Assert.IsType<OkResult>(updateEmp);
            mockRepo.Verify();
        }

        [Fact]
        public void TestDeleteEmployees()
        {
            var mockRepo = new Mock<IEmployeeService>();
            var mockIdentity = new Mock<IIdentityService>();

            var Employees = new Employee()
            {
                Id = 1,
                Name = "John",
                Department = "DotNet",
                Salary = 50000
            };
            var id = 1;
            mockRepo.Setup(repo => repo.GetEmployeeById(id)).Returns(Employees);
            mockRepo.Setup(repo => repo.DeleteEmployee(1)).Returns(true);
        
         EmployeeController empService = new EmployeeController(mockRepo.Object, mockIdentity.Object);
           var emp = empService.DeleteEmployee(id);

            // 1 object deleted, it should return 1
            Assert.Equal(true, emp);
        }
        private IEnumerable<Employee> GetTestEmployees()
        {
            return new List<Employee>()
            {
                     new Employee()
                     {
                         Id = 1,
                         Name = "John",
                         Department = "DotNet",
                         Salary = 50000
                     }

            };
        }
    }
}
