using System.Collections.Generic;
using System.Linq;
using backend_users.Data;
using backend_users.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_users.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;
        
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public UserModel Add(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(UserModel user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(UserModel user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        
        public bool CheckIfUserExists(string name)
        {
            return _context.Users.Any(u => u.UserName == name);
        }
        
        public bool CheckIfIdExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.Id);
        }

        public UserModel GetSingleUser(int id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }
        
        public UserModel GetSingleUser(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}