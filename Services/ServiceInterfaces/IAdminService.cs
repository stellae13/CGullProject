using CGullProject.Models;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// Authorize login of admin to admin portal, or deny them access.
        /// </summary>
        /// <param name="username">Username of admin user, plaintext</param>
        /// <param name="passwdMsgDigest">Hashcode of password of admin user, 
        /// represented as 64-char hex string of password's SHA256 digest.</param>
        /// <returns><para><see cref="AdminStatus.OK">Login successful</see> | 
        /// <see cref="AdminStatus.MALFORMED_MESSAGE_DIGEST">Password hashcode's hex representation malformatted</see> | 
        /// <see cref="AdminStatus.WRONG_MESSAGE_DIGEST">Wrong password hashcode</see> | 
        /// <see cref="AdminStatus.USER_DNE">Username not found</see></para>
        /// <seealso cref="AdminStatus">See AdminStatus enum typedef for more details</seealso></returns>
        public Task<AdminStatus> Login(string username, string passwdMsgDigest);
        /// <summary>
        /// Get all admin users in the system.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">IEnumerable</see> data structure containing all instances of admin users in the database.</returns>
        public Task<IEnumerable<Admins>> GetAllAdmins();
        /// <summary>
        /// Add a new admin to the system (requires authorization from existing admin, 
        /// verified by parameterizing their login credentials alongside the new account credentials).
        /// </summary>
        /// <param name="currentU">Existing admin's username.</param>
        /// <param name="currentPassWdMsgDigest">Hashcode of existing admin's password, 
        /// represented as 64B ASCII hex string of password's SHA256 MD.</param>
        /// <param name="username">Username of the new admin to add to the system.</param>
        /// <param name="passWdMsgDigest">Hashcode of existing admin's password, represented as
        /// 64B ASCII hex string of password's SHA256 MD.</param>
        /// <returns><para><see cref="AdminStatus.OK">New user successfully added</see> | <see cref="AdminStatus.LOGIN_FAILURE">Failed to authenticate existing admin's credentials</see> |
        /// <see cref="AdminStatus.MALFORMED_MESSAGE_DIGEST">New password's hashcode hex representation malformatted</see> |
        /// <see cref="AdminStatus.USERNAME_CONFLICT">New username given is already registered in the system</see></para>
        /// <seealso cref="AdminStatus">See AdminStatus enum typedef for more details</seealso></returns>
        public Task<AdminStatus> AddAdmin(string currentU, string currentPassWdMsgDigest, string username, string passWdMsgDigest);
    }
    /// <summary>
    /// These are to be returned by any implementations of the above interface. For sake of adding context to 
    /// the reason behind any method invocation failures. [value &lt; 0: call failed] [value = 0: call succeeded].
    /// </summary>
    public enum AdminStatus {

        /// <summary>Failed to log existing admin into system</summary>
        LOGIN_FAILURE = -5,

        /// <summary>Hash string formatted properly, but its corresponding password doesn't match</summary>
        WRONG_MESSAGE_DIGEST = -4,

        /// <summary>The given username does not exist in the system</summary>
        USER_DNE = -3,

        /// <summary>The new user's chosen username has already been taken by another admin.</summary>
        USERNAME_CONFLICT = -2,

        /// <summary>Hash string formatted incorrectly</summary>
        MALFORMED_MESSAGE_DIGEST = -1,

        /// <summary>Method exited successfully</summary>
        OK = 0,

    }


}
