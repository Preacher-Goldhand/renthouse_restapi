using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public ActionResult Register([FromBody] UserModel userModel)
    {
        _userService.CreateUser(userModel);
        return Ok();
    }

    [HttpPost]
    public ActionResult Login([FromBody] UserModel userModel)
    {
        string token = _userService.GenerateJwt(userModel);
        return Ok(token);
    }
}
