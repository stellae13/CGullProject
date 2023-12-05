using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IAdminService
    {
        public Task<AdminStatus> Login(string username, string passwdMsgDigest);

        public Task<IEnumerable<Admins>> GetAllAdmins();

        public Task<AdminStatus> AddAdmin(string currentU, string currentPassWdMsgDigest, string username, string passWdMsgDigest);
    }

    public enum AdminStatus {
        LOGIN_FAILURE = -5,
        WRONG_MESSAGE_DIGEST = -4,
        USER_DNE = -3,
        USERNAME_CONFLICT = -2,
        MALFORMED_MESSAGE_DIGEST = -1,
        OK = 0,

    }


}
