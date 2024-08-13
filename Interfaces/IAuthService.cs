using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
    public interface IAuthService
    {
        User AuthenticatedUser(SignInDTO item);
        User GetUserByEmail(string email);
        User GetUserByPhoneNumber(string phoneNumber);
        User GetUserByUserName(string userName);
        UsernameType ChooseUsernameType(string username);
    }
}
