using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
    {
        _userService.CreateUser(dto);
        return Ok();
    }
    [HttpPost("login")]
    public ActionResult LoginUser([FromBody] LoginUserDto dto)
    {
        string token = _userService.GenerateJwt(dto);
        return Ok(token);
    }

}
