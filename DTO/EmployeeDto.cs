using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTest.DTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateofJoining { get; set; }
        public int DepartmentId { get; set; }
      
        public DepartmentDto department { get; set; }
    }
}
