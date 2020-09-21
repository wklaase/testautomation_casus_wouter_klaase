using System.Net;
using Integrationtests.Clients.Models;
using Integrationtests.Features.BaseStepDefinitions;
using Integrationtests.Models;
using Integrationtests.Models.Enumerations;
using Integrationtests.Models.Requests;
using Integrationtests.Models.Responses;
using Newtonsoft.Json;
using NUnit.Framework;
using RestEase;
using TechTalk.SpecFlow;

namespace Integrationtests.Features.Login.StepDefinitions
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        private PostSingleUserRequestBody _postSingleUser = new PostSingleUserRequestBody        
        { 
                UserName = Constants.UserName, 
                PassWord = Constants.PassWord, 
                Role = Roles.user.ToString() 
        };

        private readonly ScenarioContext _scenarioContext;
        private Response<Token> _tokenResponse;

        public LoginSteps(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have a user profile")]
        public void CreateUserProfile() 
        {
            var response = _userBackendApiClient.AddUser(_postSingleUser).Result;
        }

        [Given(@"I have an administrator profile")]
        public void CreateAdministratorProfile() 
        {
            _postSingleUser.Role = Roles.admin.ToString();
            var response = _userBackendApiClient.AddUser(_postSingleUser).Result;
        }

        [Given(@"I am logged in as administrator")]
        public void LogInAsAdministrator() 
        {
            var response = _proxyApiClient.GetToken(
                new TokenRequestBody
                {
                    Id = (int)_scenarioContext[Constants.Id],
                    PassWord = _scenarioContext[Constants.PassWord].ToString(),
                    UserName = _scenarioContext[Constants.UserName].ToString()
                }).Result;
            var content = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<Token>(content);

            _scenarioContext[Constants.Authorization] = Constants.Bearer + " " + token.Access_Token;
        }

        [Given(@"I have a user profile that is inactive")]
        public void CreateInactiveUserProfile() 
        {
            _postSingleUser.Active = false;

            var response = _userBackendApiClient.AddUser(_postSingleUser).Result;
        }

        [When(@"I login to the IMTDb")]
        public void LogIn() 
        {
            _tokenResponse = _proxyApiClient.GetToken(new TokenRequestBody
            {
                Id = 1,
                PassWord = _postSingleUser.PassWord,
                UserName = _postSingleUser.UserName
            }).Result;
        }

        [Then(@"I expect a valid access token")]
        public void ExpectAValidAccessToken() 
        {
            Assert.IsNotNull(_tokenResponse);
            Assert.IsNotNull(_tokenResponse.ResponseMessage);

            var content = _tokenResponse.ResponseMessage.Content.ReadAsStringAsync().Result;

            Assert.IsNotEmpty(content);

            var token = JsonConvert.DeserializeObject<Token>(content);

            Assert.IsNotNull(token.Access_Token);
            Assert.IsNotEmpty(token.Access_Token);
            Assert.AreEqual(HttpStatusCode.OK, _tokenResponse.ResponseMessage.StatusCode);
        }

        [Then(@"I expect a status code in the get token response: (.*)")]
        public void ExpectAStatusCodeInTheGetTokenResponse(int statusCode) 
        {
            Assert.IsNotNull(_tokenResponse);
            Assert.IsNotNull(_tokenResponse.ResponseMessage);
            Assert.IsNotEmpty(_tokenResponse.ResponseMessage.StatusCode.ToString());
            Assert.AreEqual(statusCode, (int)_tokenResponse.ResponseMessage.StatusCode);
        }

        [Then(@"I expect an error message in the get token response: (.*)")]
        public void ExpectAnErrorMessageInTheGetTokenResponse(string errorMessage) 
        {
            var content = _tokenResponse.ResponseMessage.Content.ReadAsStringAsync().Result;
            var apiError = JsonConvert.DeserializeObject<BadRequestResponse>(content);

            Assert.AreEqual(errorMessage, apiError.Error.ToString());
        }
    }
}