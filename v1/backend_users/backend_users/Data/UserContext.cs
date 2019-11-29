using backend_users.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_users.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}