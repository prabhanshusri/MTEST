using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
    public interface IEmployeeService : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);

        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

        IEnumerable<Employee> GetCustomList();

    }
}
