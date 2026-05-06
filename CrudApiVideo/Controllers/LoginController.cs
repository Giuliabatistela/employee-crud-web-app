using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CrudApiVideo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Crud;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<bool> ValidarLogin([FromBody]Login login)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Login WHERE CPF = @cpf AND Senha = @senha";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@cpf", login.CPF);
                command.Parameters.AddWithValue("@senha", login.Senha);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return Ok(true);
                }


            }
            return BadRequest(false);
        }
    }
}
