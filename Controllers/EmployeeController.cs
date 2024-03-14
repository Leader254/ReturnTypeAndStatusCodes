using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReturnTypeAndStatusCodes.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ReturnTypeAndStatusCodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // when you return primitive or complex types directly from your actions, ASP.NET automatically wraps your response in a 200 Ok response assuming that the operation was successful
        // primitive return types may include string, integers, bools and complex as objects
        public EmployeeController() { }

        // returning a primitive type - string
        [HttpGet("Name")]
        public string GetName()
        {
            return "Return from getname";
        }

        // return a complex type - employee object
        [HttpGet("Details")]
        public Employee GetEmployeeDetails()
        {
            return new Employee()
            {
                Id = 1001,
                Name = "Anurag",
                Age = 28,
                City = "Mumbai",
                Gender = "Male",
                Department = "IT"
            };
        }

        // return a complex data collection from the controller action method
        //[HttpGet("All")]
        //public IEnumerable<Employee> GetEmployees()
        //{
        //    return new List<Employee>()
        //    {
        //        new Employee()
        //        {
        //            Id = 1001,
        //            Name = "Anurag",
        //            Age = 28,
        //            City = "Mumbai",
        //            Gender = "Male",
        //            Department = "IT"
        //        },
        //        new Employee()
        //        {
        //            Id = 1002,
        //            Name = "Pranaya",
        //            Age = 28,
        //            City = "Delhi",
        //            Gender = "Male",
        //            Department = "IT"
        //        },
        //        new Employee()
        //        {
        //            Id = 1003,
        //            Name = "Priyanka",
        //            Age = 27,
        //            City = "BBSR",
        //            Gender = "Female",
        //            Department = "HR"
        //        }
        //    };
        //}

        // IActionResult return type is used when the action method will return different types of data
        // Is capable of representing various HTTP status codes- can return OK, BadRequest, NotFound....
        [HttpGet("allEmp")]
        public IActionResult GetAllEmployees()
        {
            // returning a list of employees, if atleast one employee is present we need to return status OK with the list of employees
            // if no employee return not found
            var listEmployees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1001,
                    Name = "Anurag",
                    Age = 28,
                    City = "Mumbai",
                    Gender = "Male",
                    Department = "IT"
                },
                new Employee()
                {
                    Id = 1002,
                    Name = "Pranaya",
                    Age = 28,
                    City = "Delhi",
                    Gender = "Male",
                    Department = "IT"
                },
                new Employee()
                {
                    Id = 1003,
                    Name = "Priyanka",
                    Age = 27,
                    City = "BBSR",
                    Gender = "Female",
                    Department = "HR"
                },
            };

            if (listEmployees.Any())
            {
                return Ok(listEmployees);
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpGet("{Id}")]
        //public IActionResult GetEmployeeDetails(int id)
        //{
        //    var listEmployees = new List<Employee>()
        //    {
        //        new Employee()
        //        {
        //            Id = 1001,
        //            Name = "Anurag",
        //            Age = 28,
        //            City = "Mumbai",
        //            Gender = "Male",
        //            Department = "IT"
        //        },
        //        new Employee()
        //        {
        //            Id = 1002,
        //            Name = "Pranaya",
        //            Age = 28,
        //            City = "Delhi",
        //            Gender = "Male",
        //            Department = "IT"
        //        },
        //        new Employee()
        //        {
        //            Id = 1003,
        //            Name = "Priyanka",
        //            Age = 27,
        //            City = "BBSR",
        //            Gender = "Female",
        //            Department = "HR"
        //        },
        //    };

        //    var employee = listEmployees.FirstOrDefault(emp => emp.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(employee);
        //}

        //[HttpGet("{Id}")]
        //public ActionResult<Employee> GetEmployeeDetails(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return new Employee()
        //        {
        //            Id = 1001,
        //            Name = "Anurag",
        //            Age = 28,
        //            City = "Mumbai",
        //            Gender = "Male",
        //            Department = "IT"
        //        };
        //    }
        //}

        // IActionResult won't take in a Type T but ActionResult<T> will always have a type
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmployee()
        {
            var listEmployees = new List<Employee>()
            {
                new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR"}
            };
            if (listEmployees.Count > 0)
            {
                return Ok(listEmployees);
            }
            return NotFound();
        }

        // Task<ActionResult<T>>
        [HttpGet("Emp")]
        public async Task<ActionResult<List<Employee>>> GetEveryEmployee()
        {
            var listEmployees = new List<Employee>()
            {
                new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR"}
            };
            if (listEmployees.Count > 0)
            {
                return Ok(listEmployees);
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetSingleEmp(int id)
        {
            var listEmployees = new List<Employee>()
            {
                new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT" },
                new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR"}
            };

            var employee = listEmployees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //[HttpGet("skip")]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetPaginatedEmployees(int pageNumber)
        //{
        //    int recordsPerPage = 4;
        //    int numOfItemsToSkip = (pageNumber - 1) * recordsPerPage;
        //    List<Employee> listEmployees = EmpoyloyeeList();

        //    return listEmployees.Skip(numOfItemsToSkip).Take(recordsPerPage).ToList();

        //    //List<Employee> employees = listEmployees.OrderByDescending(s => s.Salary).Take(4).ToList();
        //    //if (employees.Count > 0)
        //    //{
        //    //    return Ok(employees);
        //    //}
        //    //return NotFound();

        //}

        private readonly List<Employee> listEmployees = new List<Employee>()
        {
                new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT", Salary = 50000 },
                new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT", Salary = 55000 },
                new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR", Salary = 60000 },
                new Employee(){ Id = 1004, Name = "John", Age = 30, City = "New York", Gender = "Male", Department = "Marketing", Salary = 70000 },
                new Employee(){ Id = 1005, Name = "Alice", Age = 25, City = "Los Angeles", Gender = "Female", Department = "Finance", Salary = 65000 },
                new Employee(){ Id = 1006, Name = "Bob", Age = 32, City = "Chicago", Gender = "Male", Department = "IT", Salary = 60000 },
                new Employee(){ Id = 1007, Name = "Emily", Age = 29, City = "San Francisco", Gender = "Female", Department = "HR", Salary = 62000 },
                new Employee(){ Id = 1008, Name = "David", Age = 27, City = "Boston", Gender = "Male", Department = "Finance", Salary = 68000 },
                new Employee(){ Id = 1009, Name = "Samantha", Age = 31, City = "Seattle", Gender = "Female", Department = "Marketing", Salary = 72000 },
                new Employee(){ Id = 1010, Name = "Michael", Age = 29, City = "Austin", Gender = "Male", Department = "IT", Salary = 58000 }
        };

        [HttpGet("pagination")]
        [SwaggerOperation(Summary = "Get paginated employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetPaginatedEmployees([FromQuery] PaginationParameters pagination)
        {
            try
            {
                int recordsPerPage = pagination.PageSize;
                int pageNumber = pagination.PageNumber;
                int numOfItemsToSkip = (pageNumber - 1) * recordsPerPage;

                var paginatedEmployees = listEmployees.Skip(numOfItemsToSkip).Take(recordsPerPage).ToList();

                if (paginatedEmployees.Any())
                {
                    return Ok(paginatedEmployees);
                }
                else
                {
                    return NotFound("No employees found for the specified page number.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
