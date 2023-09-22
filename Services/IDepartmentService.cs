using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
    public interface IDepartmentService : IRepositoryBase<Department>
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
        void CreateDepartment(Department  department);
        void UpdateDepartment(Department  department);
    }
}
