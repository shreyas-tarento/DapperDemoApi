using DapperDemoApi.IServices;
using DapperDemoApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DapperDemoApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly String _connectionString; 
        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultDbConnection");
        }
        public async Task<bool> CreateEmployee(Employee employee)
        {
            var sql = @"INSERT INTO [dbo].[Employees]
                                    ([FirstName]
                                    ,[LastName]
                                    ,[Department])
                                VALUES
                                    (@FirstName,
                                     @LastName,
                                     @Department)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var refectedCount = await connection.ExecuteAsync(sql, employee);

                return true;
            }

        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            var sql = @"DELETE FROM [dbo].[Employees] WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var refectedCount = await connection.ExecuteAsync(sql, employee);

                return employee;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var sql = @"SELECT [Id]
                                ,[FirstName]
                                ,[LastName]
                                ,[Department]
                                FROM [DapperDemo].[dbo].[Employees]";

            using(var connection = new SqlConnection(_connectionString))
            {
                var employees = await connection.QueryAsync<Employee>(sql);

                return employees;
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var sql = @"SELECT [Id]
                                ,[FirstName]
                                ,[LastName]
                                ,[Department]
                                FROM [DapperDemo].[dbo].[Employees] WHERE Id=@id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var employees = await connection.QueryAsync<Employee>(sql, new { id = id});

                return employees.FirstOrDefault();
            }
        }

        public async Task<Employee> UpdateEmployee(Employee exising, Employee employee)
        {
            var sql = @"UPDATE [dbo].[Employees]
                        SET [FirstName] = @FirstName,
                            [LastName] = @LastName,
                            [Department] = @Department
                            WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var refectedCount = await connection.ExecuteAsync(sql, employee);

                return await GetEmployee(employee.Id);
            }
        }
    }
}
