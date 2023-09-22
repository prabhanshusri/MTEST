using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Entities
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateofJoining { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
