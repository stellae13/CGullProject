using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace CGullProject.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        /// <summary>
        /// Admin service
        /// </summary>
        private readonly IAdminService _service;

       /// <summary>
       /// Constructor 
       /// </summary>
       /// <param name="service"></param>
        public AdminsController(IAdminService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all of the admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAdmins")]
        public async Task<ActionResult> GetAllAdmins()
        {
            var admins = await _service.GetAllAdmins();

            return Ok(admins);
        }

        /// <summary>
        /// Login, 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> ActionResult of Ok if valid username and password combination, otherwise returns NotFound 
        /// if username not found or BadRequest if password incorrect or invalid SHA256 MD passed through. </returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([Required] string username, [Required] [FromBody] string passMessageDigest)
        {

            
            AdminStatus stat = await _service.Login(username, passMessageDigest);
            switch (stat) 
            {
                case AdminStatus.OK:
                    return Ok("Login successful");
                case AdminStatus.USER_DNE:
                    return NotFound($"No Admin account with username, \"{username}\" found.");
                case AdminStatus.MALFORMED_MESSAGE_DIGEST:
                    return BadRequest("Malformed SHA256 message digest.");
                case AdminStatus.WRONG_MESSAGE_DIGEST:
                    return BadRequest("Incorrect password.");
                default:
                    return BadRequest();  // Will never happen but compiler doesn't know that,
                                          // only here to make compiler happy.

            }
        }

        /// <summary>
        /// Add an admin to the database 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>ActionResult type, Ok, if successful, other BadRequest if: current admin login fails, 
        /// username conflicts with existing user, or SHA256 MD format issue. </returns>
        [HttpPut("AddAdmin")]
        public async Task<IActionResult> AddAdmin([Required] string currentAdminUsername, [Required] string username, [Required] [FromBody] string passes)
        {
            string[] passesSplit = passes.Split(";");
            if (passesSplit.Length != 2)
                return BadRequest("Message digests malformatted.");
            AdminStatus stat = await _service.AddAdmin(currentAdminUsername, passesSplit[0], username, passesSplit[1]);
            switch (stat) 
            {
                case AdminStatus.OK:
                    return Ok($"User with username, \"{username}\" successfully created.");
                case AdminStatus.USERNAME_CONFLICT:
                    return BadRequest($"User with username, \"{username}\" already exists");
                case AdminStatus.MALFORMED_MESSAGE_DIGEST:
                    return BadRequest("Malformed SHA256 message digest.");
                case AdminStatus.LOGIN_FAILURE:
                    return BadRequest("Failed to log in. No user added to system.");
                default:
                    return BadRequest();  // Will never happen but compiler doesn't know that,
                                          // only here to make compiler happy.
            
            }
        }
    }
}