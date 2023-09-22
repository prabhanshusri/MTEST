using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
    public class DepartmentService : RepositoryBase<Department>, IDepartmentService
    {

        public DepartmentService(MTDBContext dbContext)
          : base(dbContext)
        {
        }
        public void CreateDepartment(Department department)
        {
            Create(department);
        }
        public void UpdateDepartment(Department department)
        {
            Update(department);
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return FindAll().OrderBy(x => x.DepartmentId).ToList();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return FindByCondition(x => x.DepartmentId == departmentId).FirstOrDefault();
        }
    }
}
