using backend_users.Services;
using backend_users.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_users.Controllers
{
    
    [Route("api/[controller]")]
    public class ValidationController : Controller
    {
        private readonly IUsersLogic _usersLogic;

        public ValidationController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        [HttpPost]
        public IActionResult Check([FromBody] ValidateUser validate)
        {
            var valid = new ValidatedUser
            {
                PassWordIsValid = _usersLogic.CheckIfPassWordIsCorrect(validate)
            };
            
            return Ok(valid);
        }
    }
}