using System.Collections.Generic;

namespace backend_users.ViewModels
{
    public class GetAllUsers
    {
        public IEnumerable<GetSingleUser> AllUsers { get; set; }
    }
}