using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
    public class EmployeeService : RepositoryBase<Employee>, IEmployeeService
    {
        public EmployeeService(MTDBContext dbContext)
         : base(dbContext)
        {
        }
        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return FindAll().OrderBy(x => x.EmployeeId).ToList();
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return FindByCondition(x => x.EmployeeId == employeeId).FirstOrDefault();
        }

        public IEnumerable<Employee> GetCustomList()
        {
            return Get(x => x.Department).ToList();
        }
    }
}
