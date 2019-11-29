using System.Collections.Generic;
using backend_users.Repository.Models;

namespace backend_users.Repository
{
    public interface IUserRepository
    {
        UserModel Add(UserModel user);
        void Update(UserModel user);
        void Delete(UserModel user);
        bool CheckIfUserExists(string name);
        bool CheckIfIdExists(int id);
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetSingleUser(int id);
        UserModel GetSingleUser(string userName);
    }
}