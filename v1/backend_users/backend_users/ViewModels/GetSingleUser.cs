using backend_users.Repository.Models;

namespace backend_users.ViewModels
{
    public class GetSingleUser : BaseModel
    {
        public string Role { get; set; }
        public bool Active { get; set; }
    }
}