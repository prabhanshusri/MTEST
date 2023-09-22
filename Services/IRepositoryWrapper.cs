using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Services
{
   public interface IRepositoryWrapper
    {
        IDepartmentService  Department { get; }
        IEmployeeService Employee { get; }

        void Save();
    }
}
