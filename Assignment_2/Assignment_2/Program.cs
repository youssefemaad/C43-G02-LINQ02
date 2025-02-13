using System;
using System.Linq;
using System.Collections.Generic;
using static Assignment_1.ListGenerator;

class Program
{
    static void Main()
    {
        int[] numbersB = { 1, 3, 5, 7, 8 };
        int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
        int[] numArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        #region LINQ - Restriction Operators
        // Find all products that are out of stock.
        var Result = ProductsList.Where(p => p.UnitsInStock == 0);
        // Find all products that are in stock and cost more than 3.00 per unit.
        Result = ProductsList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M);

        // Returns digits whose name is shorter than their value.
        var Result3 = Arr.Select((name, index) => new { name, index })
                                  .Where(x => x.name.Length < x.index)
                                  .Select(x => x.name);

        foreach (var digit in Result3)
        {
            Console.WriteLine(digit);
        }
        #endregion

        #region LINQ - Element Operator
        // Get first Product out of Stock
        var Result4 = ProductsList.FirstOrDefault(p => p.UnitsInStock == 0);

        // Return the first product whose Price > 1000, unless there is no match, in which case null is returned.
        var Result5 = ProductsList.FirstOrDefault(p => p.UnitPrice > 1000);

        // Retrieve the second number greater than 5
        var Result6 = numArr.Where(n => n > 5).ElementAt(1);


        #endregion


        #region LINQ - Aggregate Operators

        // 1.Uses Count to get the number of odd numbers in the array
        var oddNumbers = numArr.Count(n => n % 2 == 1);
        Console.WriteLine(oddNumbers);

        // 2.Return a list of customers and how many orders each has.
        var Result7 = CustomersList.Select(c => new { c.CustomerID, OrderCount = c.Orders.Count() });

        foreach (var customer in Result7)
        {
            Console.WriteLine($"Customer ID: {customer.CustomerID}, Order Count: {customer.OrderCount}");
        }

        // 3.Return a list of categories and how many products each has.
        var Result8 = ProductsList.GroupBy(p => p.Category)
                      .Select(g => new { Category = g.Key, ProductCount = g.Count() });

        foreach (var category in Result8)
        {
            Console.WriteLine($"Category: {category.Category}, Product Count: {category.ProductCount}");
        }

        // 4.Get the total of the numbers in an array.
        var Result9 = numArr.Sum();
        Console.WriteLine(Result9);

        // 5.Get the total number of characters of all words in dictionary_english.txt (Read dictionary_english.txt into Array of String First)
        string[] dictionaryWords = System.IO.File.ReadAllLines("dictionary_english.txt");
        var totalCharacters = dictionaryWords.Sum(word => word.Length);
        Console.WriteLine(totalCharacters);
        

        // 6.Get the length of the shortest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First)
        var shortestWordLength = dictionaryWords.Min(word => word.Length);
        Console.WriteLine(shortestWordLength);

        // 7.Get the length of the longest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First)
        var longestWordLength = dictionaryWords.Max(word => word.Length);
        Console.WriteLine(longestWordLength);

        // 8.Get the average length of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First)
        var averageWordLength = dictionaryWords.Average(word => word.Length);
        Console.WriteLine(averageWordLength);
        #endregion

        #region LINQ - Ordering Operators
        // 1.Sort a list of products by name.
        var Result10 = ProductsList.OrderBy(p => p.ProductName);

        // 2.Uses a custom comparer to do a case-insensitive sort of the words in an array.
        string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
        var Result11 = words.OrderBy(a => a, StringComparer.OrdinalIgnoreCase);

        foreach (var word in Result11)
        {
            Console.WriteLine(word);
        }
        // 3.Sort a list of products by units in stock from highest to lowest.
        var Result12 = ProductsList.OrderByDescending(p => p.UnitsInStock);

        // 4.Sort a list of digits, first by length of their name, and then alphabetically by the name itself.
        var Result13 = Arr.OrderBy(a => a.Length).ThenBy(a => a);

        foreach (var digit in Result13)
        {
            Console.WriteLine(digit);
        }

        // 5.Sort first by-word length and then by a case-insensitive sort of the words in an array.
        var Result14 = words.OrderBy(a => a.Length).ThenBy(a => a, StringComparer.OrdinalIgnoreCase);

        // 6.Sort a list of products, first by category, and then by unit price, from highest to lowest.
        var Result15 = ProductsList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);

        // 7.Sort first by-word length and then by a case-insensitive descending sort of the words in an array.
        var Result16 = words.OrderBy(a => a.Length).ThenByDescending(a => a, StringComparer.OrdinalIgnoreCase);

        // 8.Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array.
        var Result17 = Arr.Where(a => a[1] == 'i').Reverse();

        #endregion

        #region LINQ - Transformation Operators
        // 1.Return a sequence of just the names of a list of products.
        var Result18 = ProductsList.Select(p => p.ProductName);

        // 2.Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
        var Result19 = words.Select(a => new { Upper = a.ToUpper(), Lower = a.ToLower() });

        foreach (var word in Result19)
        {
            Console.WriteLine($"Upper: {word.Upper}, Lower: {word.Lower}");
        }

        // 3.Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.
        var Result20 = ProductsList.Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });

        foreach (var product in Result20)
        {
            Console.WriteLine($"Product Name: {product.ProductName}, Category: {product.Category}, Price: {product.Price}");
        }

        // 4.Determine if the value of int in an array matches their position in the array.
        var Result21 = numArr.Select((num, index) => new { num, index, isEqual = num == index });

        foreach (var item in Result21)
        {
            Console.WriteLine($"{item.num}: {item.num == item.index}");
        }

        // 5.Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.
        var Result22 = numbersA.SelectMany(a => numbersB, (a, b) => new { a, b }).Where(ab => ab.a < ab.b);

        Console.WriteLine("Pairs where a < b:");
        foreach (var item in Result22)
        {
            Console.WriteLine($"{item.a} is Less Than {item.b}");
        }

        // 6.Select all orders where the order total is less than 500.00.
        var Result23 = CustomersList.SelectMany(c => c.Orders, (c, o) => new { c.CustomerID, o.OrderID, o.Total }).Where(c => c.Total < 500.00M);

        foreach (var order in Result23)
        {
            Console.WriteLine($"Customer ID: {order.CustomerID}, Order ID: {order.OrderID}, Total: {order.Total}");
        }
        
        // 7.Select all orders where the order was made in 1998 or later.
        var Result24 = CustomersList.SelectMany(c => c.Orders, (c, o) => new { c.CustomerID, o.OrderID, o.OrderDate }).Where(c => c.OrderDate.Year >= 1998);

        foreach (var order in Result24)
        {
            Console.WriteLine($"Customer ID: {order.CustomerID}, Order ID: {order.OrderID}, Order Date: {order.OrderDate}");
        }
        #endregion


    }
}
