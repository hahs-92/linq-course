﻿using System.Runtime.Intrinsics.Arm;
using TCPData;
using TCPExtensions;


// PART 2 LINQ OPERATORS

List<Employee> employees = Data.GetEmployees();
List<Department> departments = Data.GetDepartments();

// Method Syntaxt 
var results = employees.Select(emp => new
{
    FullName = emp.FirstName + emp.LastName,
    AnnualSalary = emp.AnnualSalary
}).Where(e => e.AnnualSalary >= 5000);


foreach (var emp in results)
{
    Console.WriteLine($"{emp.FullName} -  $ {emp.AnnualSalary}");
}

// Query Sintaxt _ deferred execute
// Query Sintax es deferred, lo que signica que la query solo se ejecuta
// hasta que entra en un ciclo. De esta manera, esta optimizada a diferencia de
// Method Sintax. Para este ejemplo, cuando agregamos un nuevo employee a la lista
// cuando el ciclo se ejecuta, tambien incluye el employee nuevo
/*
 * immediate execute
var resulQuery = (from emp in employees
                 where emp.AnnualSalary >= 5000
                 select new
                 {
                     FullName = emp.FirstName + emp.LastName,
                     emp.AnnualSalary
                 }).ToList(); // el to list lo vuleve un immediate execute
*/
var resulQuery = from emp in employees
                 where emp.AnnualSalary >= 5000
                 select new
                 {
                     FullName = emp.FirstName + emp.LastName,
                     emp.AnnualSalary
                 };

employees.Add(new()
{
    Id = 5,
    FirstName = "Clara",
    LastName = "Witch",
    AnnualSalary = 100000.2m,
    IsManager= true,
    DepartmentId=2
});

Console.WriteLine("----------------------Query Sintaxt");
foreach (var emp in resulQuery)
{
    Console.WriteLine($"{emp.FullName} -  $ {emp.AnnualSalary}");
}


Console.WriteLine("----------------------EnumerableCollectionExtensionmethods");

var resultExt = from emp in employees.GetHighSalariedEmployees(50000)
                select new
                {
                    FullName = emp.FirstName + emp.LastName,
                    emp.AnnualSalary
                };

employees.Add(new()
{
    Id = 6,
    FirstName = "Sam",
    LastName = "Witch",
    AnnualSalary = 500001.2m,
    IsManager = true,
    DepartmentId = 3
});

foreach (var emp in resulQuery)
{
    Console.WriteLine($"{emp.FullName} -  $ {emp.AnnualSalary}");
}


// JOIN------------
Console.WriteLine("----------------JOIN");


var resultJoin = departments.Join(
    employees, 
    dep => dep.Id, 
    emp => emp.DepartmentId, 
    (dep, emp) => new
        {
            FullName = emp.FirstName + emp.LastName,
            emp.AnnualSalary,
            DepartmentName = dep.LongName
        }
    );


foreach (var emp in resultJoin)
{
    Console.WriteLine($"{emp.FullName} -  $ {emp.AnnualSalary} - { emp.DepartmentName}");
}


// METHOD SINTAXT
Console.WriteLine();
Console.WriteLine("---------------Join Group Inner --- METHOD SINTAXT");

var resultsJoinInner = departments.GroupJoin(
    employees,
    dep => dep.Id,
    emp => emp.DepartmentId,
    (dep, employessGroup) => new
        {
            Employees = employessGroup,
            DepartmentName = dep.LongName
        }
    );

foreach (var item in resultsJoinInner)
{
    Console.WriteLine($"Department Name: -  {item.DepartmentName} ");
    foreach (var emp in item.Employees)
    {
        Console.WriteLine($"{emp.FirstName} -  $ {emp.AnnualSalary} ");
    }
}



// QUERY SINTAXT
Console.WriteLine();
Console.WriteLine("---------------Join Group Inner --- QUERY SINTAXT");


var resultJGQuerySintaxt = from dept in departments
                           join emp in employees
                           on dept.Id equals emp.DepartmentId
                           into employeeGroup
                           select new
                           {
                               Employees = employeeGroup,
                               DepartmentName = dept.LongName
                           };

foreach (var item in resultJGQuerySintaxt)
{
    Console.WriteLine($"Department Name: -  {item.DepartmentName} ");
    foreach (var emp in item.Employees)
    {
        Console.WriteLine($"{emp.FirstName} -  $ {emp.AnnualSalary} ");
    }
}


Console.ReadKey();




// PARTE 1

//List<Employee> employees = Data.GetEmployees();

var filteredEmployees = employees.Filter(employee => employee.IsManager == false);

foreach (Employee employee in filteredEmployees)
{
    Console.WriteLine($"FullName:   {employee.FirstName}  {employee.LastName}");
}


//List<Department> departments = Data.GetDepartments();

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