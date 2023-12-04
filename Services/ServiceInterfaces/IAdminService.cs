using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IAdminService
    {
        public Task<bool> Login(string username, string password);

        public Task<IEnumerable<Admins>> GetAllAdmins();

        public Task<bool> AddAdmin(string currentU, string currentP, string username, string password);
    }
}
