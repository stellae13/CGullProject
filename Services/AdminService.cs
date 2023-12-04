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

        public async Task<IEnumerable<Admins>> GetAllAdmins()
        {
            IEnumerable<Admins> admin =
               await _context.Admins.ToListAsync<Admins>();


            return admin;
        }

        public async Task<bool> Login(string username, string password)
        {
            IEnumerable<Admins> admin =  _context.Admins.Where(a => a.Username == username);
            
            if(!admin.Any())
            {
                return false; // this username does not exist 
            }

            Admins a = admin.First();

            if(a.Password == password) // check to make sure passwords match
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddAdmin(string currentU, string currentP, string username, string password)
        {

            if (Login(currentU, currentP).Result) // valid current Admin 
            {
                IEnumerable<Admins> admin = _context.Admins.Where(a => a.Username == username);

                // make sure no username exists 
                if (!admin.Any())
                {
                    Admins newAdmin = new()
                    {
                        Username = username,
                        Password = password
                    };

                    // add and save changes
                    await _context.Admins.AddAsync(newAdmin);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                    return false;
            }
        }
    }

    public enum AdminErrors {
        STATUS_WRONG_MESSAGE_DIGEST = -4,
        STATUS_USER_DNE = -3,
        STATUS_USERNAME_CONFLICT = -2,
        STATUS_MALFORMED_MESSAGE_DIGEST = -1,
        STATUS_OK = 0,

    }
}
