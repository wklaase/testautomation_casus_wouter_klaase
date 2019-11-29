using Newtonsoft.Json;

namespace backend_users.ViewModels
{
    public class ValidatedUser
    {
        [JsonProperty(PropertyName = "passWordIsValid")]
        public bool PassWordIsValid { get; set; }
    }
}