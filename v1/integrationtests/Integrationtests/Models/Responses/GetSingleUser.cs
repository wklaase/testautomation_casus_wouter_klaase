namespace Integrationtests.Models.Responses
{
    public class GetSingleUser : BaseModel
    {
        public string Role { get; set; }
        public bool Active { get; set; }
    }

}