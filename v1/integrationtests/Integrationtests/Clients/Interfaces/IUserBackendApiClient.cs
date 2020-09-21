using System.Threading.Tasks;
using Integrationtests.Models.Requests;
using Integrationtests.Models.Responses;
using RestEase;

namespace Integrationtests.Clients.Interfaces
{
    [AllowAnyStatusCode]
    [BasePath("api/")]
    public interface IUserBackendApiClient
    {
        /// <summary>Adds the user.</summary>
        /// <param name="body">The body.</param>
        /// <returns>A <see cref="Task{ApiResponse}"/>object</returns>
        [Post("users")]
        Task<Response<GetSingleUser>> AddUser([Body]PostSingleUserRequestBody body);
    }
}