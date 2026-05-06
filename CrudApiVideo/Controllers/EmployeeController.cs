using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CrudApiVideo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Crud;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }



        [HttpGet(Name = "GetEmployees")]
        public IEnumerable<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["Name"].ToString(),
                        Position = reader["Position"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                    employees.Add(employee);
                }
                reader.Close();
            }

            return employees;
        }



        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult GetEmployeeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["Name"].ToString(),
                        Position = reader["Position"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                    reader.Close();

                    return Ok(employee);
                }

                reader.Close();
            }

            return NotFound();
        }



        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Employees (Name, Position, Salary) VALUES (@Name, @Position, @Salary)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            
            }

            return BadRequest();
        }



        [HttpPut("{id}")]
        [HttpPut]

        public ActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE Employees SET Name = @Name, Position = @Position, Salary = @Salary WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return NotFound();
        }
    }
}
