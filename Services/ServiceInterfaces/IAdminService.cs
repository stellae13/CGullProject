using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IAdminService
    {
        public Task<bool> Login(string username, string password);
    }
}
