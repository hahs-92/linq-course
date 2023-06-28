using TCPData;
using TCPExtensions;


List<Employee> employees = Data.GetEmployees();

var filteredEmployees = employees.Filter(employee => employee.IsManager == false);

foreach (Employee employee in filteredEmployees)
{
    Console.WriteLine($"FullName:   {employee.FirstName}  {employee.LastName}");
}


List<Department> departments = Data.GetDepartments();

var filteredDepartments = departments.Filter(department => department.ShortName == "TE");

foreach(Department department in filteredDepartments)
{
    Console.WriteLine($"Department: {department.LongName}");
}



// with Linq
Console.WriteLine("Linq");
Console.WriteLine("-----------");
var filteredWithLinq = employees.Where(employee => employee.IsManager == true);

foreach(Employee employee in filteredWithLinq)
{
    Console.WriteLine($"FullName:   {employee.FirstName}  {employee.LastName}");
}

// Linq TSQL sintax
Console.WriteLine("Linq TSQL sintax");
Console.WriteLine("-------------");
var resultList = from emp in employees
                 join dept in departments
                 on emp.DepartmentId equals dept.Id
                 select new
                 {
                     FullName = emp.FirstName + emp.LastName,
                     AnnualSalary = emp.AnnualSalary,
                     Department = dept.LongName
                 };

foreach(var emp in resultList)
{
    Console.WriteLine($"FullName:  {emp.FullName}");
    Console.WriteLine($"Department:  {emp.Department}");
}


var averageAnnualSalary = resultList.Average(a => a.AnnualSalary);
var highestSalary = resultList.Max(a => a.AnnualSalary);
var lowestSalary = resultList.Min(a => a.AnnualSalary);

Console.WriteLine("--------------");
Console.WriteLine($"Operations : {averageAnnualSalary} - { highestSalary} - {lowestSalary} ");


Console.ReadKey();