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
    public class AdminsController
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

        [HttpGet]
        public async Task<bool> Login([Required] string username, [Required]string password)
        {
            return await _service.Login(username, password);
        }
    }
}
