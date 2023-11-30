using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> GetAllItems()
        {
            var admins = await _service.GetAllAdmins();

            return Ok(admins);
        }

        /// <summary>
        /// Login, 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if valid username and password combination, false otherwise</returns>
        [HttpGet("Login")]
        public async Task<bool> Login([Required] string username, [Required]string password)
        {
            return await _service.Login(username, password);
        }

        /// <summary>
        /// Add an admin to the database 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPut("AddAdmin")]
        public async Task<bool> AddAdmin([Required] string currentAdminUsername, [Required] string currentAdminPassword, [Required] string username, [Required] string password)
        {
            return await _service.AddAdmin(currentAdminUsername, currentAdminPassword, username, password);
        }
    }
}