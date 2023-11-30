using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<Admins> admin =  _context.Admins.Where(a => a.Username == username);
                //?? throw new KeyNotFoundException($"User with {username} not found");
            
            if(!admin.Any())
            {
                return false;
            }

            Admins a = admin.First();

            if(a.Password == password)
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
