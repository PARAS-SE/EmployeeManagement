using EmployeeManagement.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.IServices
{
    /// <summary>
    /// Employee service interface
    /// </summary>
    public interface IEmployeeService
    {

        IEnumerable<Employee> GetEmployee();
        Employee GetEmployeeById(int id);
        bool AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);

    }
}
