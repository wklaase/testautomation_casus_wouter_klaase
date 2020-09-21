using Newtonsoft.Json;

namespace Integrationtests.Models.Requests
{
    public class PostSingleUserRequestBody
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("password")]
        public string PassWord { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}