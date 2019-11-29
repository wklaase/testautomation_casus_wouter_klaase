using System;

namespace backend_users.Services.Extensions
{
    public static class Validators
    {
        public static void ValidateRoleForNewUser(this UsersLogic usersLogic, string role)
        {
            if (role.ToLowerInvariant() != "admin" && role.ToLowerInvariant() != "user")
            {
                throw new ArgumentException($"Role: {role} not valid must be either admin or user");
            }
        }
    }
}