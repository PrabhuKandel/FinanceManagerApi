using System.Linq;
using System.Runtime.Intrinsics.X86;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqController : ControllerBase
    {
        [HttpGet("word-starting-with-a")]
        public IActionResult GetWordsStartingWithA()
        {
            List<string> words = new List<string> { "Apple", "Banana", "Avocado", "Grapes", "Apricot", "Orange" };

            var result = words.Where(word => word.StartsWith("A")).ToList();
            return Ok(result);
        }

        [HttpGet("sort-name-alphabetically")]
        public IActionResult SortNameAlphabetically()
        {
            List<string> names = new List<string> { "Zara", "Michael", "Anna", "John", "Chris" };

        //  The lambda n => n . This part tells LINQ what to sort by. n represents each item in the list.

        //=> n means use the value itself as the key for sorting. Because each item in this list is already a string, it sorts based on alphabetical order.

            var result = names.OrderByDescending(name=>name).ToList();
            return Ok(result);
        }


        [HttpGet("check-if-all-numbers-are-positive")]
        public IActionResult CheckIfAllNumbersArePositive()
        {

            List<int> nums = new List<int> { 3, 5, -1, 10, 8 };
            var isPositive = nums.All(num => num >= 0);
            return Ok(isPositive);
        }

        [HttpGet("longest-word")]
        public IActionResult GetLongestWord()
        {

            List<string> text = new List<string>{ "elephant", "cat", "hippopotamus", "dog", "giraffe" };
            var longestWord = text.OrderByDescending(t => t.Length).First();
            return Ok(longestWord);

        }

        [HttpGet("group-student-by-class")]
        public IActionResult GroupStudentByClass()
        {

             List<Student> students = new List<Student>
        {
            new Student { Name="Alice", Class="A" },
            new Student { Name="Bob", Class="B" },
            new Student { Name="Charlie", Class="A" },
            new Student { Name="David", Class="C" },
            new Student { Name="Eve", Class="B" }
        };

            var result =
                students.GroupBy(student => student.Class)
                .Select(group => new
                {
                    Class = group.Key,
                    Students = group.Select(s=>s.Name).ToList()
                });

            return Ok(result);


        }

        [HttpGet("top-3-highest-salary")]
        public IActionResult Top3HighestSalary()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { Name="Aiden", Salary=4000 },
                new Employee { Name="Bella", Salary=7000 },
                new Employee { Name="Chris", Salary=5500 },
                new Employee { Name="Diana", Salary=8000 },
                new Employee { Name="Ethan", Salary=3000 }
            };


            var result = employees.OrderByDescending(emp => emp.Salary).Take(3);

            return Ok(result);
        }

        [HttpGet("total-price-per-category")]
        public IActionResult TotalPricePerCategory()
        {
             List<Product> products = new List<Product>
            {
                new Product { Name="Laptop", Price=1200, Category="Electronics" },
                new Product { Name="Phone",  Price=800,  Category="Electronics" },
                new Product { Name="Shirt",  Price=40,   Category="Clothing" },
                new Product { Name="Jeans",  Price=60,   Category="Clothing" },
                new Product { Name="Bread",  Price=3,    Category="Food" }
            };

            var result = products.GroupBy(product => product.Category)
                          .Select(g => new
                          {

                              Category = g.Key,
                              TotalPrice = g.Sum(p => p.Price)
                          });

            return Ok(result);
        }


        [HttpGet("most-frequent-number")]
        public IActionResult MostFrequentNumber()
        {
            List<int> seq = new List<int> { 5, 1, 5, 2, 3, 5, 3, 1, 2 };

            var mostFrequentNumber = seq.GroupBy(num => num).OrderByDescending(g=>g.Count()).First().Key;
     
            return Ok(mostFrequentNumber);
        }

    }

    public class Student
    {
        public  required string Name { get; set; }
        public required string Class { get; set; }
    }

    public class Employee
    {
        public required string Name { get; set; }
        public decimal  Salary { get; set; }
    }

    public class Product
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }

    }



}
