using Demo_1;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Demo_1.ListGenerator;

class Program
{

    static void Main()
    {
        List<int> Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        #region What is var?
        // var name = "LINQ";
        // // var can be used to store any type of data, and the type is determined at compile time, and can't be changed later or assigned to null.

        // // Employee employee = new Employee(Id = 1, Name = "John", Salary = 1000);
        // // object Emp = new { Id = 1, Name = "John", Salary = 1000 };
        // // Console.WriteLine(Emp.Id); //invalid
        // var Emp01 = new { Id = 1, Name = "John", Salary = 1000 }; //AnonymousType0`3
        // Console.WriteLine(Emp01.GetType().Name); //valid
        // var Emp02 = new { Id = Emp01.Id, Name = Emp01.Name, Salary = 2000 };
        // var Emp03 = Emp01 with { Salary = 3000 };

        // var Emp04 = new { id = 1, name = "John", salary = 1000 }; //AnonymousType1`3 change in name of properties change the AnonymousType
        // var Emp05 = new { name = "omar", id = 20, salary = 2000 }; //AnonymousType2`3 change in order of properties change the AnonymousType
        #endregion

        #region What is LINQ?
        // LinQ => Language Integrated Query
        // List<int> OddNum = Numbers.Where(num => num % 2 == 1).ToList();

        // foreach (var num in OddNum)
        // {
        //     Console.WriteLine(num);
        // }

        // Sequence => The Objects that implement IEnumerable<T> interface
        // Local -> Static ,XML [L2XML], Object, Database
        // Remote -> L2EF, L2SQL, L2WCF, L2WebService



        #endregion

        #region LINQ Query Syntax [Fluent Syntax, Query Syntax]

        #region 1.Fluent Syntax
        //1.1 call "LinQ Operator" as Static Method
        // var OddNum1 = Enumerable.Where(Numbers, num => num % 2 == 1);
        // //1.2 call "LinQ Operator" as Extension Method
        // OddNum1 = Numbers.Where(num => num % 2 == 1);
        #endregion

        #region 2.Query Syntax

        // var OddNum2 = from num in Numbers
        //               where num % 2 == 1
        //               select num;

        // foreach (var num in OddNum2) {
        //     Console.WriteLine(num);
        // }

        // Must start with From
        // Must end with Select or GroupBy
        #endregion

        #endregion

        #region Extension Ways

        #region Deferred Execution
        // var OddNum3 = Numbers.Where(num => num % 2 == 1);// Deferred Execution
        // var OddNum4 = Numbers.Where(num => num % 2 == 1).ToList(); //Immediate Execution

        // Numbers.AddRange(new int[] { 10, 11, 12, 13, 14, 15 });

        // foreach (var num in OddNum3)
        // {
        //     Console.WriteLine(num); // 1,3,5,7,9,11,13,15
        // }

        // foreach (var num in OddNum4)
        // {
        //     Console.WriteLine(num); // 1,3,5,7,9
        // }
        #endregion
        #endregion

        #region Get Products Out of Stock
        //Fluent Syntax
        var Result = ProductsList.Where(p => p.UnitsInStock == 0);

        //Query Syntax
        Result = from p in ProductsList
                 where p.UnitsInStock == 0
                 select p;

        foreach (var item in Result)
        {
            Console.WriteLine(item);
        }
        #endregion

        #region Ex02
        //Fluent Syntax
        Result = ProductsList.Where(p => p.UnitsInStock > 0 && p.Category == "Meat/Poultry");

        //Query Syntax
        Result = from p in ProductsList
                 where p.UnitsInStock > 0 && p.Category == "Meat/Poultry"
                 select p;

        foreach (var item in Result)
        {
            Console.WriteLine(item);
        }
        #endregion

        #region Indexed Where

        // Fluent Syntax
        var Result2 = ProductsList.Where((p, i) => p.UnitsInStock == 0 && i < 10);
        // search in first 10 elements

        foreach (var item in Result2)
        {
            Console.WriteLine(item);
        } 
        #endregion

        #region Transformation [Projection] Operators [Select, SelectMany]

        #region Select
        //Fluent Syntax
        var Result3 = ProductsList.Select(p => p.ProductName);

        //Query Syntax
        Result3 = from p in ProductsList
                  select p.ProductName;

        // Fluent Syntax
        var Result4 = CustomersList.Select(c => c.CustomerName);

        // Query Syntax
        Result4 = from c in CustomersList
                  select c.CustomerName;
        #endregion

        #region SelectMany
        // Fluent Syntax
        var Result5 = CustomersList.SelectMany(c => c.Orders);

        // Query Syntax
        Result5 = from c in CustomersList
                  from o in c.Orders
                  select o;
        #endregion

        #region Select Product Id and Product Name

        // Fluent Syntax
        var Result6 = ProductsList.Select(p => new {p.ProductID ,p.ProductName } );

        // Query Syntax
        Result6 = from p in ProductsList
                  select new { p.ProductID, p.ProductName };

        #endregion

        #region Select Product In Stock And Apply Discount 10% on its Price

        // Fluent Syntax
        var Result7 = ProductsList.Where(p => p.UnitsInStock > 0)
                                  .Select(p => new { p.ProductID, p.ProductName, p.UnitPrice, DiscountedPrice = p.UnitPrice - (p.UnitPrice*0.1M)});

        // Query Syntax
        Result7 = from p in ProductsList
                  where p.UnitsInStock > 0
                  select new { p.ProductID, p.ProductName, p.UnitPrice, DiscountedPrice = p.UnitPrice - (p.UnitPrice * 0.1M) };


        #endregion

        #endregion


        #region Ording Operators [ Asc, Desc, Rev, ThenBy, ThenByDesc]

        #region Asc
        // Fluent Syntax
        var Result8 = ProductsList.OrderBy(p => p.UnitPrice);

        // Query Syntax
        Result8 = from p in ProductsList
                  orderby p.UnitPrice
                  select p;

        #endregion

        #region Desc
        // Fluent Syntax
        var Result9 = ProductsList.OrderByDescending(p => p.UnitPrice);

        // Query Syntax
        Result9 = from p in ProductsList
                  orderby p.UnitPrice descending
                  select p;

        #endregion

        #region Reverse
        // Fluent Syntax
        var Result10 = ProductsList.Where(p => p.UnitsInStock == 0).Reverse();

        // Query Syntax
        Result10 = (from p in ProductsList
                    where p.UnitsInStock == 0
                    select p).Reverse();

        #endregion

        #region Product Ordered by Price Asc and Number of Items in Stock

        // Fluent Syntax
        var Result11 = ProductsList.OrderBy(p => p.UnitPrice).ThenBy(p => p.UnitsInStock);

        // Query Syntax
        Result11 = from p in ProductsList
                   orderby p.UnitPrice, p.UnitsInStock
                   select p;

        #endregion

        #endregion

        #region Element Operators - Immediate Execution [Valid Only with Fluent Syntax]

        #region Fluent Syntax
        var Result12 = ProductsList.First();
        Result12 = ProductsList.First(p => p.UnitsInStock == 0);
        Result12 = ProductsList.FirstOrDefault();
        Result12 = ProductsList.Last();
        Result12 = ProductsList.Last(p => p.UnitsInStock == 0);
        Result12 = ProductsList.LastOrDefault();

        List<Product> prod = new List<Product>(); //Empty List
        // Result12 = prod.First(); //InvalidOperationException
        // Result12 = prod.First(p => p.UnitsInStock == 0); //InvalidOperationException
        Result12 = prod.FirstOrDefault(); //null
        // Result12 = prod.Last(); //InvalidOperationException
        // Result12 = prod.Last(p => p.UnitsInStock == 0); //InvalidOperationException
        Result12 = prod.LastOrDefault(); //null

        Result12 = ProductsList.ElementAt(0); // Get Element at Index 0
        Result12 = ProductsList.ElementAtOrDefault(0); // Get Element at Index 0 or null if not found

        Result12 = ProductsList.Single(p => p.ProductID == 1); // Get Element with ProductID = 1 or throw exception if not found or more than one
        Result12 = ProductsList.SingleOrDefault(p => p.ProductID == 1); // Get Element with ProductID = 1 or null if not found or more than one

        Console.WriteLine(Result12?.ProductName ?? "Not Found");
        #endregion

        #region Hybrid Syntax

        var Result13 = (from p in ProductsList
                       where p.UnitsInStock == 0
                       select new
                       {
                           p.ProductID,
                           p.ProductName,
                           p.UnitsInStock
                       }).First();

        Result13 = (from p in ProductsList
                    where p.UnitsInStock == 0
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitsInStock
                    }).FirstOrDefault();

        Console.WriteLine(Result13?.ProductName ?? "Not Found");

        #endregion

        #endregion

        #region Aggregate Operators - Immediate Execution

        // Count
        var Result14 = ProductsList.Count; // List Operator
        Result14 = ProductsList.Count(p => p.UnitsInStock == 0); // LINQ Operator

        // Max
        Result14 = ProductsList.Max(p => p.ProductName.Length);

        // Min
        var minlength = ProductsList.Min(p => p.ProductName.Length);
        var Result15 = (from p in ProductsList
                        where p.ProductName.Length == minlength
                        select p).First();

        Console.WriteLine(Result14);

        // Sum
        Result14 = ProductsList.Sum(p => p.UnitsInStock);


        string[] Names = { "Ahmed", "Ali", "Omar", "Mohamed"};

        var Result16 = Names.Aggregate((str1, str2) => $"{str1} {str2}");

        /*
         * str1 = "Ahmed"   , str2 = "Ali" 
         * str1 => "Ahmed Ali"
         * 
         * str1 = "Ahmed Ali"   , str2 = "Omar" 
         * str1 => "Ahmed Ali Omar" 
         * 
         * str1 = "Ahmed Ali Omar"   , str2 = "Mohamed" 
         * str1 => "Ahmed Ali Omar Mohamed"
         */
        Console.WriteLine(Result16);

        #endregion
    }
}
