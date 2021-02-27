using EmployeeManagement.WebApi.IServices;
using EmployeeManagement.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        EmployeedbContext dbContext;
        public EmployeeService(EmployeedbContext _db)
        {
            dbContext = _db;
        }
        public IEnumerable<Employee> GetEmployee()
        {
            var employee = dbContext.Employee.ToList();
            return employee;
        }
        public bool AddEmployee(Employee employee)
        {
            if (employee != null)
            {
                dbContext.Employee.Add(employee);
                dbContext.SaveChanges();
            }
            return true;
        }
        public bool UpdateEmployee(Employee employee)
        {
            dbContext.Entry(employee).State = EntityState.Modified;
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
          
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            var employee = dbContext.Employee.FirstOrDefault(x => x.Id == id);
            dbContext.Entry(employee).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }
        public Employee GetEmployeeById(int id)
        {
            var employee = dbContext.Employee.FirstOrDefault(x => x.Id == id);
            return employee;
        }
    }
}
