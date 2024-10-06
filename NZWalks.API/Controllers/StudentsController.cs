using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    //https://localhost:7284/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7284/api/students
        [HttpGet]
        public IActionResult GetStudents()
        {
            string[] studentNames = new string[] { "Ayca", "Onur", "Almina" };

            return Ok(studentNames);
        }
    }
}
