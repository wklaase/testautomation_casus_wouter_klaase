namespace backend_users.Services
{
    public static class PassWordHasher
    {
        public static string SetPassword(this UsersLogic usersLogic, string submittedPassword)
        {
            // hash and save a password
            return BCrypt.Net.BCrypt.HashPassword(submittedPassword);
        }

        public static bool DoesPasswordMatch(this UsersLogic usersLogic, string submittedPassword, string hashedPassword)
        {
            // check a password
            return BCrypt.Net.BCrypt.Verify(submittedPassword, hashedPassword);
        }
    }
}