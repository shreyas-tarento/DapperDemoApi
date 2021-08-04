using DapperDemoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoApi.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee exising, Employee employee);
        Task<Employee> DeleteEmployee(Employee employee);
    }
}
