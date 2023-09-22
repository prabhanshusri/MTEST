using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MTDBContext _dbContext;
        private IDepartmentService _department;
        private IEmployeeService _employee;

        public RepositoryWrapper(MTDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDepartmentService Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentService(_dbContext);
                }
                return _department;
            }
        }

        public IEmployeeService Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeService(_dbContext);
                }
                return _employee;
            }
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

