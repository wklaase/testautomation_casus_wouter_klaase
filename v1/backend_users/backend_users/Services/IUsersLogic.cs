using backend_users.Repository.Models;
using backend_users.ViewModels;

namespace backend_users.Services
{
    public interface IUsersLogic
    {
        GetAllUsers GetAllUsersFromDataBase();
        GetSingleUser GetByUserName(string username);
        GetSingleUser GetById(int id);
        void UpdateUser(UpdateSingleUser user, int id);
        void DeleteUser(int id);
        void CreateANewUser(PostSingleUser user);
        bool CheckIfPassWordIsCorrect(ValidateUser validate);
    }
}
