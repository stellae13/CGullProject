using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CGullProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly ShopContext _context;

        private static bool PassMatch(byte[] a, byte[] b) {
            if ((b.Length != 32) || (a.Length != b.Length))
                return false;
            for (int i = 0; i < 4; ++i)
            {
                // BitConverter.ToUInt<bitlength> (allegedly according to Bing) allows for raw reinterpretation of bits
                // that comprise a memory block as a different-sized int type, like how you can do in C/C++
                // (ex:
                // uint8_t foo[256];
                // uint64_t bar = *(&foo[16]);
                // -------- equivalent in C#:
                // byte foo[256];
                // UInt64 bar = BitConverter.ToUint64(foo, 16));
                // )
                // This sort of raw reinterpretation of a block of memory turns this 32-member byte array equality check
                // into a 4-member ulong array (4 ulongs = 32 bytes / 8 bytes per ulong) equality check without any
                // of the significant overhead we'd otherwise risk incurring if we were to use more-abstracted/higher-level
                // methods of conversion/typecasting.
                if (BitConverter.ToUInt64(a, i << 3) == BitConverter.ToUInt64(b, i << 3))
                    continue;
                return false;
            }
                    
            return true;
        }

        private static byte[]? SHA256DigestBytesFromHex(string passWdMsgDigest) {
            if (passWdMsgDigest.Length != 64)
               return null;
            char tmp1, tmp2;  // Declared outside of loop body to save from overhead
                              // of having vars constantly being realloc'd each loop.
            byte[] ret = new byte[32];
            for (int i=0; i < 32; ++i)
            {
                // Hoping that using all of these bitwise operations in lieu of
                // regular arithmetic operators will offer at least some level
                // of noticeable improvement.

                // Using i << 1 and i << 1 | 1 in order to get every char in the array as pairs of chars.
                tmp1 = char.ToUpper(passWdMsgDigest[i << 1]);  // i << 1 = 2*i
                tmp2 = char.ToUpper(passWdMsgDigest[(i << 1) | 1]);  // (i << 1) | 1 = 2*i + 1
                if (!char.IsAsciiHexDigit(tmp1) || !char.IsAsciiHexDigit(tmp2))
                    return null;
                // Derive the byte value from the hex digit pair using 4b shift left for hex value most significant digit
                // and bitwise OR that to hex value of the least significant digit. Find hex value by minusing out the
                // char's offset from base value. For hex digits 0-9 its value is then ASCII_NumberValue(digit) - ASCII_NumberValue('0'),
                // which yields range from 0 to 9. For hex digits 'A'-'F' its value is then 10 + ASCII(digit) - ASCII('A') to yeild values
                // from 10 to 15.
                ret[i] = (byte)((((tmp1 < 'A') ? (tmp1 - '0') : ((char)10 + tmp1 - 'A')) << 4) & 0xF0);
                ret[i] |= (byte)(((tmp2 < 'A') ? (tmp2 - '0') : ((char)10 + tmp2 - 'A')) & 0x0F);
            }
            return ret;
        }
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

        public async Task<AdminStatus> Login(string username, string passWdMsgDigest)
        {
            byte[]? password = await Task.Run(() => SHA256DigestBytesFromHex(passWdMsgDigest));
            if (password is null)
                return AdminStatus.MALFORMED_MESSAGE_DIGEST;

            if (password.Length != 32)
                return AdminStatus.MALFORMED_MESSAGE_DIGEST;

            IEnumerable<Admins> admin = _context.Admins.Where(a => a.Username == username);

            if (admin.IsNullOrEmpty())
            {
                return AdminStatus.USER_DNE; // this username does not exist 
            }

            Admins a = admin.First();

            if (await Task.Run(() => PassMatch(a.Password, password)))
            {
                return AdminStatus.OK;
            }
            else
            {
                return AdminStatus.WRONG_MESSAGE_DIGEST;
            }
        }
        public async Task<AdminStatus> AddAdmin(string currentU, string currentPasswdMsgDigest, string username, string passWdMsgDigest)
        {
            if ((await Login(currentU, currentPasswdMsgDigest)) != AdminStatus.OK)
                return AdminStatus.LOGIN_FAILURE;
            
            byte[]? newUserPass = await Task.Run(() => SHA256DigestBytesFromHex(passWdMsgDigest));
            if (newUserPass is null)
                return AdminStatus.MALFORMED_MESSAGE_DIGEST;


            IEnumerable<Admins> admin = _context.Admins.Where(a => a.Username == username).Select(a=>a);
            // make sure no username exists 
            if (!admin.Any())
            {
                Admins newAdmin = new()
                {
                    Username = username,
                    Password = newUserPass
                };

                // add and save changes
                await _context.Admins.AddAsync(newAdmin);
                await _context.SaveChangesAsync();

                return AdminStatus.OK;
            }
            else
            {
                return AdminStatus.USERNAME_CONFLICT;
            }
        }
    }

}
