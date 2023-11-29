using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;

namespace CGullProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly ShopContext _context;

        public AdminService(ShopContext context)
        {
            _context = context;
        }

        public async Task<bool> Login(string username, string password)
        {
            Admins? admin = _context.Admins.Where(a => a.Username == username).First()
                ?? throw new KeyNotFoundException($"User with {username} not found"); 

            if(admin.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
