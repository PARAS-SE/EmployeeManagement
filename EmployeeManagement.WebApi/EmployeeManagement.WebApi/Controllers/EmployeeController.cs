using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.WebApi.Common;
using EmployeeManagement.WebApi.IServices;
using EmployeeManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IIdentityService _identityService;
      
        public EmployeeController(IEmployeeService employee, IIdentityService identityService)
        {
            employeeService = employee;
            _identityService = identityService;
        }

        /// <summary>
        /// Registered users can login. On login Token will received to access methods.
        /// </summary>
        /// <param name="model"> login model having user name and passwords.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        //[Route("/Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var authResponse = _identityService.LoginAsync(model);
            if (!authResponse.Success)
            {
                return BadRequest(new Common.AuthFailResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                Success = true
            });
        }

        /// <summary>
        /// Admin user can fetch employee list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        //[Route("api/Employee/GetEmployee")]
        [Authorize(Roles = Role.Admin)]
        public IEnumerable<Employee> GetEmployee()
        {
            return employeeService.GetEmployee();
        }

        /// <summary>
        /// New employee can be added by this API.
        /// </summary>
        /// <param name="employee"> it will return added employee.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        //[Route("api/Employee/AddEmployee")]
        //[Authorize(Roles = Role.User)]
        public bool AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        /// <summary>
        /// Employee records can be edited by Update API.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
       // [Route("api/Employee/EditEmployee")]
        public bool UpdateEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }

        /// <summary>
        /// Employee Record can be deleted based on ID.
        /// </summary>
        /// <param name="id">employeed id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        //[Route("api/Employee/DeleteEmployee")]
        public bool DeleteEmployee(int id)
        {
            return employeeService.DeleteEmployee(id);
        }

        /// <summary>
        /// Employee Record can be fetched by Id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        //[Route("api/Employee/GetEmployeeId")]
        public Employee GetEmployeeId(int id)
        {
            return employeeService.GetEmployeeById(id);
        }
    }
}
