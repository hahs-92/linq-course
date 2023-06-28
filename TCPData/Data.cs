using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPData
{
    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee employee = new()
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Hernandez",
                AnnualSalary = 6699.3m,
                IsManager = true,
                DepartmentId = 1
            };

            Employee employee2 = new()
            {
                Id = 2,
                FirstName = "Jess",
                LastName = "Jhonson",
                AnnualSalary = 2899.9m,
                IsManager = false,
                DepartmentId = 2
            };

            Employee employee3 = new()
            {
                Id = 3,
                FirstName = "Jose",
                LastName = "Gomez",
                AnnualSalary = 5699.9m,
                IsManager = false,
                DepartmentId = 1
            };

            Employee employee4 = new()
            {
                Id = 4,
                FirstName = "Miguel",
                LastName = "Ruiz",
                AnnualSalary = 1299.9m,
                IsManager = false,
                DepartmentId = 3
            };

            employees.Add(employee);
            employees.Add(employee2);
            employees.Add(employee3);
            employees.Add(employee4);

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new();

            Department department = new()
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Recourses"
            };
            departments.Add(department);

            department = new()
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);

            department = new()
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }
    }
}