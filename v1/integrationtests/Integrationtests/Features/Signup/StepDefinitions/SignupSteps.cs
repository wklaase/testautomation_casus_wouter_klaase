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

namespace Integrationtests.Features.Signup.StepDefinitions
{
    [Binding]
    public class SignupSteps : BaseSteps 
    {
        private PostSingleUserRequestBody _postSingleUser = new PostSingleUserRequestBody 
        { 
            UserName = Constants.UserName, 
            PassWord = Constants.PassWord 
        };

        private Response<GetSingleUser> _postUserResponse;
        private readonly ScenarioContext _scenarioContext;

        public SignupSteps(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I use password (.*) and username (.*)")]
        public void UsePasswordAndUsername(string password, string username) 
        {
            _postSingleUser = new PostSingleUserRequestBody 
            { 
                UserName = username, 
                PassWord = password 
            };
        }

        [Given(@"I use a username that already exists")]
        public void UseAUsernameThatAlreadyExists() 
        {
            var response = _proxyApiClient.AddUser(new PostSingleUserRequestBody 
            { 
                UserName = Constants.UserName, 
                PassWord = Constants.PassWord 
            }).Result;
        }

        [When(@"I sign up as a user")]
        public void SignUpAsAUser() 
        {
            var httpContent = new PostSingleUserRequestBody 
            { 
                UserName = _postSingleUser.UserName, 
                PassWord = _postSingleUser.PassWord 
            };

            _postUserResponse = _proxyApiClient.AddUser(httpContent).Result;

            // to test the new user endpoint, set role to 'user' to check if default role user is set as expected in the response.
            _scenarioContext[Constants.Role] = Roles.user.ToString();
        }

        [Given(@"I have an administrator profile with password (.*) and username (.*)")]
        public void CreateAdminProfileWithPasswordAndUsername(string password, string username) 
        {
            var response = _userBackendApiClient.AddUser(
                new PostSingleUserRequestBody
                {
                    UserName = username,
                    PassWord = password,
                    Active = true,
                    Role = Roles.admin.ToString()
                }).Result;

            _scenarioContext[Constants.UserName] = username;
            _scenarioContext[Constants.PassWord] = password;
            _scenarioContext[Constants.Id] = 1;
        }

        [When(@"I create the administrator profile")]
        public void CreateAdministratorProfile() 
        {
            _postUserResponse = _proxyApiClient.AddAdmin(_postSingleUser, _scenarioContext[Constants.Authorization].ToString()).Result;
            _scenarioContext[Constants.Role] = Roles.admin.ToString(); ;
        }

        [Then(@"I expect that the profile is created succesfully")]
        public void ExpectThatTheProfileIsCreatedSuccesfully() 
        {
            Assert.IsNotNull(_postUserResponse);
            Assert.IsNotNull(_postUserResponse.ResponseMessage);

            var content = _postUserResponse.ResponseMessage.Content.ReadAsStringAsync().Result;

            Assert.IsNotEmpty(content);

            var successResponse = JsonConvert.DeserializeObject<GetSingleUser>(content);

            Assert.AreEqual(_postSingleUser.UserName, successResponse.Username);
            Assert.AreEqual(_scenarioContext[Constants.Role], successResponse.Role);
            Assert.AreEqual(HttpStatusCode.OK, _postUserResponse.ResponseMessage.StatusCode);
        }

        [Then(@"I expect a status code in the create user response: (.*)")]
        public void ExpectAStatusCode(int statusCode) 
        {
            Assert.IsNotNull(_postUserResponse);
            Assert.IsNotNull(_postUserResponse.ResponseMessage);
            Assert.IsNotEmpty(_postUserResponse.ResponseMessage.StatusCode.ToString());
            Assert.AreEqual(statusCode, (int)_postUserResponse.ResponseMessage.StatusCode);
        }

        [Then(@"I expect an error message in the create user response: (.*)")]
        public void ExpectAnErrorMessage(string errorMessage) 
        {
            var content = _postUserResponse.ResponseMessage.Content.ReadAsStringAsync().Result;
            var apiError = JsonConvert.DeserializeObject<BadRequestResponse>(content);

            Assert.AreEqual(errorMessage, apiError.Error.ToString().Trim());
        }
    }
}