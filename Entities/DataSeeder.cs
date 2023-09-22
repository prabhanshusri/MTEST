using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTest.Entities
{
    public class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {


            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<MTDBContext>();

                context.Database.EnsureCreated();

                if (!context.Departments.Any())
                {
                    var departments = new List<Department>
                {
                    new Department { Name="Development" },
                    new Department { Name="Testing" },
                    new Department { Name="UI-UX" },
                    new Department { Name="Network" },

                };

                    context.Departments.AddRange(departments);
                    context.SaveChanges();
                }

                if (!context.Employees.Any())
                {
                    var employees = new List<Employee>
                {
                    new Employee { Name="Prabhanshu Srivastava", EmployeeCode="EMP0001",DateofJoining=Convert.ToDateTime("22-06-2019"), DepartmentId=4},
                    new Employee {  Name="Aditya Srivastava", EmployeeCode="EMP0002",DateofJoining=Convert.ToDateTime("22-07-2020"), DepartmentId=3},
                    new Employee { Name="Sonu Srivastava", EmployeeCode="EMP0003",DateofJoining=Convert.ToDateTime("22-08-2021"), DepartmentId=2},
                    new Employee {  Name="Gyaan Srivastava", EmployeeCode="EMP0004",DateofJoining=Convert.ToDateTime("22-08-2022"), DepartmentId=1},
                };

                    context.Employees.AddRange(employees);
                    context.SaveChanges();
                }

            }

        }
    }
}
