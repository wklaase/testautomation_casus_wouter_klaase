using System;
using System.Linq;
using backend_users.Middleware;
using backend_users.Repository;
using backend_users.Repository.Models;
using backend_users.Services.Extensions;
using backend_users.ViewModels;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace backend_users.Services
{
    public class UsersLogic : IUsersLogic
    {
        private readonly IUserRepository _userRepository;

        public UsersLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetAllUsers GetAllUsersFromDataBase()
        {
            var models = _userRepository.GetAllUsers();
            var returnModels = models.Select(model => new GetSingleUser
                {
                    Id = model.Id,
                    Username = model.UserName,
                    Active = model.Active,
                    Role = model.Role
                })
                .ToList();

            var returnModel = new GetAllUsers
            {
                AllUsers = returnModels
            };

            return returnModel;
        }
        
        public GetSingleUser GetByUserName(string username)
        {
            var model = _userRepository.GetSingleUser(username.ToLowerInvariant());
            if (model == null)
            {
                throw new HttpNotFoundException($"User with username: {username} was not found");
            }

            var userModel = new GetSingleUser
            {
                Id = model.Id,
                Username = model.UserName,
                Active = model.Active,
                Role = model.Role
             };

            return userModel;
        }

        public GetSingleUser GetById(int id)
        {
            var model = _userRepository.GetSingleUser(id);
            if (model == null)
            {
                throw new HttpNotFoundException($"User with id: {id} was not found");
            }

            var idModel = new GetSingleUser
            {
                Id = model.Id,
                Username = model.UserName,
                Active = model.Active,
                Role = model.Role
            };

            return idModel;
        }

        public void CreateANewUser(PostSingleUser user)
        {
            CheckIfUserNameIsTaken(user.UserName.ToLowerInvariant());
            this.ValidateRoleForNewUser(user.Role);
            
            var hashedPassword = this.SetPassword(user.PassWord);
            var userToPost = new UserModel
            {
                UserName = user.UserName.ToLowerInvariant(),
                Active = user.Active,
                Role = user.Role,
                PassWord = hashedPassword
            };
           
            _userRepository.Add(userToPost);
        }

        public void UpdateUser(UpdateSingleUser user, int id)
        {
            if (id != null)
            {
                this.CheckIfUserExists(id);
            }

            var userToUpdate = this._userRepository.GetSingleUser(id);
            userToUpdate.Active = user.Active;
            
            this._userRepository.Update(userToUpdate);
        }

        public void DeleteUser(int id)
        {
            if (id != null)
            {
                this.CheckIfUserExists(id);
            }
            
            var userToDelete = this._userRepository.GetSingleUser(id);

            if (userToDelete.Active)
            {
                throw new ArgumentException($"User: {id} cannot be deleted because the user is still active");
            }
            this._userRepository.Delete(userToDelete);
        }
        
        public bool CheckIfPassWordIsCorrect(ValidateUser validate)
        {
            bool validPassword = false;
            CheckIfUserExists(validate.UserName);
            var userFromDatabase = _userRepository.GetSingleUser(validate.UserName);

            return this.DoesPasswordMatch(validate.PassWord, userFromDatabase.PassWord);
        }

        private void CheckIfUserNameIsTaken(string username)
        {
            var userExists = _userRepository.CheckIfUserExists(username);
            if (userExists)
            {
                throw new ArgumentException($"User: {username} already exists");
            }
        }
        
        private void CheckIfUserExists(int id)
        {
            var userExists = _userRepository.CheckIfIdExists(id);
            if (!userExists)
            {
                throw new HttpNotFoundException($"Id: {id} not found");
            }
        }
        
        private void CheckIfUserExists(string username)
        {
            var userExists = _userRepository.CheckIfUserExists(username);
            if (!userExists)
            {
                throw new HttpNotFoundException($"Username: {username} not found");
            }
        }

    }
}