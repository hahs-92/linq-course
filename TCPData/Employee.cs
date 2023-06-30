using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPData
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class EmployerComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            return  x?.Id == y?.Id &&
                    x?.FirstName == y?.FirstName && 
                    x?.LastName == y?.LastName &&
                    x?.AnnualSalary == y?.AnnualSalary;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public static class EnumerableCollectionExtensionmethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees, decimal highSalary)
        {
            foreach (Employee employee in employees)
            {
                if (employee.AnnualSalary >= highSalary)
                    yield return employee;
            }
        }
    }
}
