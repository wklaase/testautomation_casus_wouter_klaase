using System.Threading.Tasks;
using Integrationtests.Models.Requests;
using Integrationtests.Models.Responses;
using RestEase;

namespace Integrationtests.Clients.Interfaces
{
    [AllowAnyStatusCode]
    [BasePath("v1/proxy/")]
    public interface IProxyApiClient
    {
        // Token Methods

        /// <summary>Gets the token.</summary>
        /// <param name="body">The body.</param>
        /// <returns>A <see cref="Task{ApiResponse}"/>object.</returns>
        [Post("tokens/")]
        Task<Response<Token>> GetToken([Body] TokenRequestBody body);

        // User Methods

        /// <summary>Create a new user in the backend.</summary>
        /// <param name="body">The body.</param>
        /// <returns>A <see cref="Task{ApiResponse}"/>object.</returns>
        [Post("users/")]
        Task<Response<GetSingleUser>> AddUser([Body]PostSingleUserRequestBody body);

        /// <summary>Create a new admin in the backend.</summary>
        /// <param name="body">The body.</param>
        /// <param name="authorization"> The authorization.</param>
        /// <returns>A <see cref="Task{ApiResponse}"/>object.</returns>
        [Post("users/admins")]
        Task<Response<GetSingleUser>> AddAdmin([Body]PostSingleUserRequestBody body, [Header("Authorization")] string authorization);
    }
}