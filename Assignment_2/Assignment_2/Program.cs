using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_2;
using static Assignment_2.ListGenerator;

class Program
{
    static void Main()
    {
        #region LINQ - Aggregate Operators

        // 1. Get the total units in stock for each product category.
        System.Console.WriteLine("1.1.-----------------------------------------------");

        var Result01 = ProductsList.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, TotalUnitsInStock = g.Sum(p => p.UnitsInStock) });

        foreach (var item in Result01)
        {
            Console.WriteLine($"Category: {item.Category}, Total Units In Stock: {item.TotalUnitsInStock}");
        }

        // 2. Get the cheapest price among each category's products.
        System.Console.WriteLine("1.2.------------------------------------------------");

        var Result02 = ProductsList.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) });

        foreach (var item in Result02)
        {
            Console.WriteLine($"Category: {item.Category}, Cheapest Price: {item.CheapestPrice}");
        }

        // 3. Get the products with the cheapest price in each category (Use Let).
        System.Console.WriteLine("1.3.------------------------------------------------");

        var Result03 = from p in ProductsList
                       group p by p.Category into g
                       let minPrice = g.Min(p => p.UnitPrice)
                       select new { Category = g.Key, CheapestProducts = g.Where(p => p.UnitPrice == minPrice) };

        foreach (var item in Result03)
        {
            Console.WriteLine($"Category: {item.Category}, Cheapest Product: {string.Join(", ", item.CheapestProducts.Select(p => p.ProductName))}");
        }

        // 4. Get the most expensive price among each category's products.
        System.Console.WriteLine("1.4.------------------------------------------------");

        var Result04 = ProductsList.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, MostExpensivePrice = g.Max(p => p.UnitPrice) });

        foreach (var item in Result04)
        {
            Console.WriteLine($"Category: {item.Category}, Most Expensive Price: {item.MostExpensivePrice}");
        }

        // 5. Get the products with the most expensive price in each category.
        System.Console.WriteLine("1.5.------------------------------------------------");

        var Result05 = from p in ProductsList
                       group p by p.Category into g
                       let maxPrice = g.Max(p => p.UnitPrice)
                       select new { Category = g.Key, MostExpensiveProducts = g.Where(p => p.UnitPrice == maxPrice) };

        foreach (var item in Result05)
        {
            Console.WriteLine($"Category: {item.Category}, Most Expensive Product: {string.Join(", ", item.MostExpensiveProducts.Select(p => p.ProductName))}");
        }
        // 6. Get the average price of each category's products.
        System.Console.WriteLine("1.6.------------------------------------------------");

        var Result06 = ProductsList.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) });

        foreach (var item in Result06)
        {
            Console.WriteLine($"Category: {item.Category}, Average Price: {item.AveragePrice}");
        }

        #endregion

        #region LINQ - Set Operators

        // 1. Find the unique Category names from Product List.
        System.Console.WriteLine("2.1.------------------------------------------------");

        var Result07 = ProductsList.Select(p => p.Category).Distinct();

        foreach (var item in Result07)
        {
            Console.WriteLine(item);
        }
        // 2. Produce a Sequence containing the unique first letter from both product and customer names.
        System.Console.WriteLine("2.2.------------------------------------------------");

        var Result08 = ProductsList.Select(p => p.ProductName[0]).Union(CustomersList.Select(c => c.CustomerName[0]));

        foreach (var item in Result08)
        {
            Console.WriteLine(item);
        }
        // 3. Create one sequence that contains the common first letter from both product and customer names.
        System.Console.WriteLine("2.3.------------------------------------------------");

        var Result09 = ProductsList.Select(p => p.ProductName[0]).Intersect(CustomersList.Select(c => c.CustomerName[0]));

        foreach (var item in Result09)
        {
            Console.WriteLine(item);
        }
        // 4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.
        System.Console.WriteLine("2.4.------------------------------------------------");

        var Result10 = ProductsList.Select(p => p.ProductName[0]).Except(CustomersList.Select(c => c.CustomerName[0]));

        foreach (var item in Result10)
        {
            Console.WriteLine(item);
        }
        // 5. Create one sequence that contains the last three characters in each name of all customers and products, including any duplicates.
        System.Console.WriteLine("2.5.------------------------------------------------");

        var Result11 = ProductsList.Select(p => p.ProductName.Substring(p.ProductName.Length - 3)).Union(CustomersList.Select(c => c.CustomerName.Substring(c.CustomerName.Length - 3)));

        foreach (var item in Result11)
        {
            Console.WriteLine(item);
        }

        #endregion

        #region LINQ - Partitioning Operators

        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        // 1. Get the first 3 orders from customers in Washington.
        System.Console.WriteLine("3.1.------------------------------------------------");

        var Result12 = CustomersList.Where(c => c.Country == "Washington").SelectMany(c => c.Orders).Take(3);

        foreach (var item in Result12)
        {
            Console.WriteLine(item.OrderID);
        }
        // 2. Get all but the first 2 orders from customers in Washington.
        System.Console.WriteLine("3.2.------------------------------------------------");

        var Result13 = CustomersList.Where(c => c.Country == "Washington").SelectMany(c => c.Orders).Skip(2);
        // 3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
        System.Console.WriteLine("3.3.------------------------------------------------");

        var Result14 = numbers.TakeWhile((n, i) => n >= i);

        foreach (var item in Result14)
        {
            Console.WriteLine(item);
        }
        // 4. Get the elements of the array starting from the first element divisible by 3.
        Console.WriteLine("3.4.------------------------------------------------");

        var Result15 = from n in numbers
                       where n % 3 == 0 && n != 0
                       select n;

        foreach (var item in Result15)
        {
            Console.WriteLine(item);
        }
        // 5. Get the elements of the array starting from the first element less than its position.
        System.Console.WriteLine("3.5.------------------------------------------------");

        var Result16 = numbers.SkipWhile((n, i) => n >= i);

        foreach (var item in Result16)
        {
            Console.WriteLine(item);
        }

        #endregion

        #region LINQ - Quantifiers

        // 1. Determine if any of the words in dictionary_english.txt contain the substring 'ei'.
        // System.Console.WriteLine("4.1.------------------------------------------------");

        string[] dictionary = System.IO.File.ReadAllLines("dictionary_english.txt");
        var Result17 = dictionary.Any(w => w.Contains("ei"));

        Console.WriteLine(Result17);
        // 2. Return a grouped list of products only for categories that have at least one product that is out of stock.
        System.Console.WriteLine("4.2.------------------------------------------------");

        var Result18 = ProductsList.GroupBy(p => p.Category)
            .Where(g => g.Any(p => p.UnitsInStock == 0));

        foreach (var item in Result18)
        {
            Console.WriteLine($"Category: {item.Key}");
            foreach (var product in item)
            {
                Console.WriteLine($"\t\tProduct: {product.ProductName} - Units In Stock: {product.UnitsInStock}");
            }
        }
        // 3. Return a grouped list of products only for categories that have all of their products in stock.
        System.Console.WriteLine("4.3.------------------------------------------------");

        var Result19 = ProductsList.GroupBy(p => p.Category)
            .Where(g => g.All(p => p.UnitsInStock > 0));

        foreach (var item in Result19)
        {
            Console.WriteLine($"Category: {item.Key}");
            foreach (var product in item)
            {
                Console.WriteLine($"\t\tProduct: {product.ProductName} - Units In Stock: {product.UnitsInStock}");
            }
        }

        #endregion

        #region LINQ - Grouping Operators

        List<int> numberList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        string[] Arr = { "from", "salt", "earn", "last", "near", "form" };

        // 1. Use group by to partition a list of numbers by their remainder when divided by 5.
        System.Console.WriteLine("5.1.------------------------------------------------");

        var Result20 = numberList.GroupBy(n => n % 5);

        foreach (var item in Result20)
        {
            Console.WriteLine($"Remainder: {item.Key}");
            foreach (var number in item)
            {
                Console.WriteLine($"\tNumber: {number}");
            }
        }
        // 2. Use group by to partition a list of words by their first letter using dictionary_english.txt.
        System.Console.WriteLine("5.2.------------------------------------------------");

        // Display only first 10 words in each group
        var Result21 = dictionary.GroupBy(w => w[0])
                     .Select(g => new { FirstLetter = g.Key, Words = g.Take(5) });

        foreach (var item in Result21)
        {
            Console.WriteLine($"First Letter: {item.FirstLetter}");
            foreach (var word in item.Words)
            {
                Console.WriteLine($"\tWord: {word}");
            }
        }
        // 3. Use Group By with a custom comparer that matches words that consist of the same characters together.
        System.Console.WriteLine("5.3.------------------------------------------------");

        var Result22 = Arr.GroupBy(w => w, new AnagramEqualityComparer());

        foreach (var item in Result22)
        {
            Console.WriteLine($"Item: {item.Key}");
            foreach (var word in item)
            {
                Console.WriteLine($"\tWord: {word}");
            }
        }

        #endregion
    }
}
