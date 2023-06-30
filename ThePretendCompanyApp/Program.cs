using System.Collections;
using System.Runtime.Intrinsics.Arm;
using TCPData;
using TCPExtensions;

List<Employee> employees = Data.GetEmployees();
List<Department> departments = Data.GetDepartments();


// PART 3
// // LINQ OPERATORS

// ELEMENT OPERATORS

// si no encuentra el item, genera una exception
var employeeAt = employees.ElementAt(2);

// si no encuentra una coincidencia devuelve null o zero o un 
// por defecto dependiendo el tipo
var employeeAtDefault = employees.ElementAtOrDefault(9);

var itemFounded = employees.First(e => e.DepartmentId == 2);
var itemFoundedDefault = employees.FirstOrDefault(e => e.DepartmentId == 2);

var lastItem = employees.Last(e => e.DepartmentId == 2);
var lastItemDefault = employees.LastOrDefault(e => e.DepartmentId == 2);

var itemSingle = employees.Single(e => e.DepartmentId == 2);
var itemSingleDefault = employees.SingleOrDefault(e => e.DepartmentId == 2);



//FILTER OPERATIONS

ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

// query
var stringResult = from s in mixedCollection.OfType<string>()
                   select s;

// method
var resultMethod = mixedCollection.OfType<string>();


foreach (var item in stringResult)
{
    Console.WriteLine(item);
}

Console.WriteLine();

foreach (var item in resultMethod)
{
    Console.WriteLine(item);
}

//QUANTIFY OPERATORS

var isTrueAll = employees.All(e => e.AnnualSalary > 2000);
var isTrueAny = employees.Any(e => e.AnnualSalary > 2000);

Employee employeeToSearch = new()
{
    Id = 2,
    FirstName = "Jess",
    LastName = "Jhonson",
    AnnualSalary = 2899.9m,
    IsManager = false,
    DepartmentId = 2
};

var containEmployee = employees.Contains(employeeToSearch, new EmployerComparer());

Console.WriteLine($" isTrueAll. {isTrueAll}");
Console.WriteLine($" isTrueAny. {isTrueAny}");
Console.WriteLine($" contain. {containEmployee}");
Console.WriteLine();
Console.ReadKey();

// GROUP BY OPERATIONS
// deferred execute
var resultGroup = from emp in employees
                  orderby emp.DepartmentId
                  group emp by emp.DepartmentId;

foreach (var result in resultGroup)
{
    Console.WriteLine($"Department Id: {result.Key} ");
    foreach(Employee emp in result)
    {
        Console.WriteLine($"{emp.FirstName} -  $ {emp.AnnualSalary}");
    }
}

// ToLookup Operator
// immediate execute
Console.WriteLine();
Console.WriteLine("---------------ToLookUp");
var resultGroupToLookUp =employees.OrderBy(e => e.DepartmentId).ToLookup(x => x.DepartmentId);


foreach (var result in resultGroupToLookUp)
{
    Console.WriteLine($"Department Id: {result.Key} ");
    foreach (Employee emp in result)
    {
        Console.WriteLine($"{emp.FirstName} -  $ {emp.AnnualSalary}");
    }
}


// SORTING OPERATIONS
// METHOD SINTAX 
Console.WriteLine("METHOD SINTAX - ORDER - THEMBY");
Console.WriteLine();

var resultsSorting = employees.Join(
        departments,
        emp => emp.DepartmentId,
        dep => dep.Id,
        (emp, dep) => new
        {
            emp.Id,
            emp.FirstName,
            emp.LastName,
            emp.AnnualSalary,
            emp.DepartmentId,
            DepartmentName = dep.LongName
        }).OrderBy(o => o.DepartmentId)
        .ThenBy(o => o.AnnualSalary);

foreach(var result in resultsSorting)
{
    Console.WriteLine($"{result.FirstName} -  $ {result.AnnualSalary} - { result.DepartmentName }");
}

// QUERY SINTAX
Console.WriteLine("QUERY SINTAX - ORDER - THEMBY");
Console.WriteLine();

var resultSortingQuery = from emp in employees
                         join dep in departments
                         on emp.DepartmentId equals dep.Id
                         orderby emp.DepartmentId, emp.AnnualSalary descending
                         select new
                         {
                             emp.Id,
                             emp.FirstName,
                             emp.LastName,
                             emp.AnnualSalary,
                             emp.DepartmentId,
                             DepartmentName = dep.LongName
                         };


foreach (var result in resultSortingQuery)
{
    Console.WriteLine($"{result.FirstName} -  $ {result.AnnualSalary} - {result.DepartmentName}");
}


Console.ReadKey();


// PART 2 LINQ OPERATORS

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