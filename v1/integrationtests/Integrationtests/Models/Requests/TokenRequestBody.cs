using Newtonsoft.Json;

namespace Integrationtests.Models.Requests
{
    public class TokenRequestBody
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("password")]
        public string PassWord { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}