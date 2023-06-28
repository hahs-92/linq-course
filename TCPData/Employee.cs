using System;
using System.Collections.Generic;
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

    public static class EnumerableCollectionExtensionmethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} - {employee.LastName}");

                if (employee.AnnualSalary >= 50000)
                    yield return employee;
            }
        }
    }
}
