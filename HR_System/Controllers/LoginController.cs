using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {

        [HttpPost(Name = "Login")]
        public IActionResult LoginResult([FromBody] LoginRequest request)
        {
            if (true) {
                return Ok(new { Success = true, Message = "Login successful" });
            } else {
                return Ok(new { Success = false, Message = "Login unsuccessful" });
            }
        }
    }
}
