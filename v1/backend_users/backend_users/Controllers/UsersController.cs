using System.Net;
using backend_users.Services;
using backend_users.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_users.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersLogic _usersLogic;

        public UsersController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _usersLogic.GetAllUsersFromDataBase();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleUser(int id)
        {
            var result = _usersLogic.GetById(id);
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] PostSingleUser user)
        {
            _usersLogic.CreateANewUser(user);
            var userToReturn = _usersLogic.GetByUserName(user.UserName);
            return CreatedAtRoute(new {username = user.UserName}, userToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UpdateSingleUser user, int id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented, "");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented, "");
        }
    }
}