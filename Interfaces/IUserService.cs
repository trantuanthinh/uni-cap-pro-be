using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
    // DONE
    public interface IUserService
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(Guid id);
        Task<bool> CreateUser(User item);
        Task<bool> UpdateUser(User item, User patchItem);
        Task<bool> DeleteUser(User item);
        Task<bool> IsUserUniqueAsync(User user); //check whether if user is duplicated - email, phone number, username
    }
}
